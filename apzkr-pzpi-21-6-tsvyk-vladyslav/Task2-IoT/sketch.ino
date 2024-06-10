#include <WiFi.h>
#include <HX711.h>
#include <DHTesp.h>
#include <SD.h>
#include "sensor_send_service.h"
#include "agregated_sensor_data_service.h"
#include "config_manager.h"
#include "config_structure.h"
#include "time_util.h"

const char* ssid = "Wokwi-GUEST";
const char* password = "";
const char* configFile = "/config.json";

const float BETA = 3950;
const float WEIGHT_COEF = 419;

const int CS_PIN = 5;
const int DHT_PIN = 22;
const int HX_DT_PIN = 4;
const int HX_SCK_PIN = 0;
const int BUTTON_PIN = 25;
const int LED_PIN = 32;
const int SETUP_BUTTON_PIN = 26;


DHTesp dhtSensor;
HX711 loadSensor;
int button_state;
int setup_button_state;
int led_state = LOW;

ConfigManager configManager;
ServerInteractionService serverInteractionService;
AgregatedSensorDataService agregatedSensorDataService(5);


void setupWiFi() {
  Serial.println("Connecting to wifi...");
  while (WiFi.status() != WL_CONNECTED) {
    WiFi.begin(ssid, password, 6);
    delay(1000);
    Serial.println("Retry..");
  }

  Serial.println("Connected to WiFi");
}

void syncTime() {
  configTime(0, 0, "pool.ntp.org", "time.nist.gov");
  Serial.print("Waiting for time");
  time_t now = time(nullptr);
  while (now < 8 * 3600 * 2) {
      Serial.print(".");
      delay(1000);
      now = time(nullptr);
  }

  Serial.println();
  Serial.println("Time synchronized");
}

void setup() {
  Serial.begin(115200);
  Serial.println("Esp32 started.");
  
  // WiFi setup 
  setupWiFi();

  // DHT setup (humidity and temp)
  dhtSensor.setup(DHT_PIN, DHTesp::DHT22);
  Serial.println("DHT setup completed");

  // HX711/710 setup (weight/load sensor)
  loadSensor.begin(HX_DT_PIN, HX_SCK_PIN);
  //loadSensor.set_scale(2280.f);
  loadSensor.tare();
  Serial.println("HX setup completed");

  // Button and LED setup
  pinMode(BUTTON_PIN, INPUT_PULLUP);
  pinMode(LED_PIN, OUTPUT);
  pinMode(SETUP_BUTTON_PIN, INPUT_PULLUP);

  // Config initilizing
  if (!SD.begin(CS_PIN)) {
    Serial.println("Card initialization failed!");
    while (true);
  }
  configManager.loadFromFile(configFile);

  Config config = configManager.getConfig();
  String serverConfigUrl = config.serverUrl + "/api/Apiary/hive/configuration/" + config.hiveId;
  bool serverConfigResult = configManager.loadFromServer(serverConfigUrl.c_str());
  if(serverConfigResult == false) {
    Serial.println("An error occurred while retrieving the configuration from the server. Restart the application and try again");
    while(true) {
      
    }
  } 

  configManager.printCurrentConfig();

  serverInteractionService.initialize(config.serverUrl);
  Serial.println("Server url initialized as " + config.serverUrl);

  // Time configuration
  syncTime();
  Serial.println("Initialization done.");
}

void loop() {
  // weight calibration
  button_state = digitalRead(BUTTON_PIN);
  if (button_state == LOW) {
    Serial.println("Weight calibration");
    loadSensor.tare();
    led_state = HIGH;
    Serial.println("Weight calibration completed");
  }
  else {
    led_state = LOW;
  }

  // setup from server
  setup_button_state = digitalRead(SETUP_BUTTON_PIN);
  if (setup_button_state == LOW) {
    Serial.println("Getting setup Data from server");
  
    Config config = configManager.getConfig();
    String serverConfigUrl = config.serverUrl + "/api/Apiary/hive/configuration/" + config.hiveId;
    bool serverConfigResult = configManager.loadFromServer(serverConfigUrl.c_str());
    if(serverConfigResult == false) {
      Serial.println("An error occurred while retrieving the configuration from the server. Restart the application and try again");
      while(true) {
        
      }
    } 
    configManager.printCurrentConfig();

  }

  digitalWrite(LED_PIN, led_state);

  TempAndHumidity data = dhtSensor.getTempAndHumidity();
  float weightData = loadSensor.get_units() / WEIGHT_COEF;
  String currentTime = getUtcTime();
  Config config = configManager.getConfig();
  Serial.print("--- ");
  Serial.print(currentTime);
  Serial.println(" ---");
  Serial.println("Temp: " + String(data.temperature, 2) + "°C");
  Serial.println("Humidity: " + String(data.humidity, 1) + "%");
  Serial.println("Weight: " + String(weightData) + "kg");

  bool tempToSend = agregatedSensorDataService.addTempReading(data.temperature);
  if(tempToSend) {
    float agregatedTemp = agregatedSensorDataService.getAverageTemp();
    agregatedSensorDataService.compareToCritical("temp", config.criticalTempHigh, config.criticalTempLow, agregatedTemp);
    serverInteractionService.sendSensorData(agregatedTemp, config.tempSensorId, config.hubId, currentTime);
    agregatedSensorDataService.resetTemp();
  }

  bool humidityToSend = agregatedSensorDataService.addHumidityReading(data.humidity);
  if(humidityToSend) {
    float agregatedHumidity = agregatedSensorDataService.getAverageHumidity();
    agregatedSensorDataService.compareToCritical("humidity", config.criticalHumidityHigh, config.criticalHumidityLow, agregatedHumidity);
    serverInteractionService.sendSensorData(agregatedHumidity, config.humiditySensorId, config.hubId, currentTime);
    agregatedSensorDataService.resetHumidity();
  }

  bool weightToSend = agregatedSensorDataService.addWeightReading(weightData);
  if(weightToSend) {
    float agregatedWeight = agregatedSensorDataService.getAverageWeight();
    serverInteractionService.sendSensorData(agregatedWeight, config.weightSensorId, config.hubId, currentTime);
    agregatedSensorDataService.resetWeight();
  }
  delay(2500);
}