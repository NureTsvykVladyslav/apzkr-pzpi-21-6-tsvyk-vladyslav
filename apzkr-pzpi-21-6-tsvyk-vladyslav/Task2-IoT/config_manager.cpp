#include <ArduinoJson.h>
#include <FS.h>
#include <WiFi.h>
#include <SD.h>
#include <HTTPClient.h>
#include "config_manager.h"

bool ConfigManager::loadFromFile(const char* filename) {
    File file = SD.open(filename, FILE_READ);
    if (!file) {
        Serial.print("Failed to open config file; filename: ");
        Serial.println(filename);
        return false;
    }

    size_t size = file.size();
    std::unique_ptr<char[]> buf(new char[size]);

    file.readBytes(buf.get(), size);
    file.close();

    StaticJsonDocument<512> doc;
    DeserializationError error = deserializeJson(doc, buf.get());

    if (error) {
        Serial.print("deserializeJson() failed: ");
        Serial.println(error.c_str());
        return false;
    }

    config_.criticalHumidityHigh = doc["criticalHumidityHigh"].as<float>();
    config_.criticalHumidityLow = doc["criticalHumidityLow"].as<float>();
    config_.criticalTempHigh = doc["criticalTempHigh"].as<float>();
    config_.criticalTempLow = doc["criticalTempLow"].as<float>();
    config_.humiditySensorId = doc["humiditySensorId"].as<String>();
    config_.weightSensorId = doc["weightSensorId"].as<String>();
    config_.tempSensorId = doc["tempSensorId"].as<String>();
    config_.hiveId = doc["hiveId"].as<String>();
    config_.serverUrl = doc["serverUrl"].as<String>();

    Serial.println("Config loaded from file");
    return true;
}

bool ConfigManager::loadFromServer(const char* serverUrl) {
    if (WiFi.status() != WL_CONNECTED) {
        Serial.println("WiFi not connected");
        return false;
    }

    HTTPClient http;
    Serial.print("Getting configuration from ");
    Serial.println(serverUrl);
    http.begin(serverUrl);
    int httpResponseCode = http.GET();

    if (httpResponseCode > 0) {
        String payload = http.getString();
        Serial.println("Received payload: " + payload);

        StaticJsonDocument<512> doc;
        DeserializationError error = deserializeJson(doc, payload);

        if (error) {
            Serial.print("deserializeJson() failed: ");
            Serial.println(error.c_str());
            return false;
        }

        if(httpResponseCode == 404) {
          Serial.println("Error while configuring");
          Serial.println(doc["error"].as<String>());
          return false;
        }

        config_.hiveId = doc["hiveId"].as<String>();
        config_.humiditySensorId = doc["humiditySensorId"].as<String>();
        config_.weightSensorId = doc["weightSensorId"].as<String>();
        config_.tempSensorId = doc["tempSensorId"].as<String>();
        config_.hubId = doc["hubId"].as<String>();
        config_.criticalHumidityHigh = doc["criticalHumidityHigh"].as<float>();
        config_.criticalHumidityLow = doc["criticalHumidityLow"].as<float>();
        config_.criticalTempHigh = doc["criticalTempHigh"].as<float>();
        config_.criticalTempLow = doc["criticalTempLow"].as<float>();
        return true;
    } else {
        Serial.print("Error on HTTP request: ");
        Serial.println(httpResponseCode);
        return false;
    }

    http.end();
}

const Config& ConfigManager::getConfig() const {
    return config_;
}

void ConfigManager::printCurrentConfig() {
  Serial.println("Current configuration:");
  Serial.println("> Hive Id: " + config_.hiveId);
  Serial.println("> Humidity Sensor Id: " + config_.humiditySensorId);
  Serial.println("> Weight Sensor Id: " + config_.weightSensorId);
  Serial.println("> Temperature Sensor Id: " + config_.tempSensorId);
  Serial.println("> Hub Id: " + config_.hubId);
  Serial.println("> Critical Humidity High: " + String(config_.criticalHumidityHigh));
  Serial.println("> Critical Humidity Low: " + String(config_.criticalHumidityLow));
  Serial.println("> Critical Temperature High: " + String(config_.criticalTempHigh));
  Serial.println("> Critical Temperature Low: " + String(config_.criticalTempLow));
  Serial.println("> Server URL: " + config_.serverUrl);
}