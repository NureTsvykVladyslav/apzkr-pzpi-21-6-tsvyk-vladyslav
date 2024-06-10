#include "sensor_send_service.h"

void ServerInteractionService::initialize(const String& serverUrl) {
  serverUrl_ = serverUrl;
}

bool ServerInteractionService::sendSensorData(float sensorValue, const String& sensorId, const String& hubId, const String& timestamp) {
    if (WiFi.status() == WL_CONNECTED) {
        HTTPClient http;
        String fullUrl = String(serverUrl_) + "/api/Sensor/readings/hub/" + hubId;
        http.begin(fullUrl);
        http.addHeader("Content-Type", "application/json");

        String payload = "[{";
        payload += "\"value\": " + String(sensorValue) + ",";
        payload += "\"sensorId\": \"" + sensorId + "\",";
        payload += "\"readingDate\": \"" + timestamp + "\"";
        payload += "}]";
        Serial.println(fullUrl);
        Serial.println(payload);
        int httpResponseCode = http.POST(payload);
        bool success = httpResponseCode > 0 && httpResponseCode < 300;

        if (success) {
            Serial.println("Data sent successfully: " + String(httpResponseCode));
        } else {
            Serial.println("Error sending data: " + String(httpResponseCode));
        }
        
        http.end();
        return success;
    } else {
        Serial.println("Wi-Fi not connected");
        return false;
    }
}

