#ifndef DATA_COLLECTION_SERVICE_H
#define DATA_COLLECTION_SERVICE_H

#include <Arduino.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include <WiFi.h>
#include "config_from_server.h"

/**
 * @brief Class for sending received data from sensors to server.
 */
class ServerInteractionService {
  public:
    void initialize(const String& serverUrl);
    bool sendSensorData(float sensorValue, const String& sensorId, const String& hubId, const String& timestamp);
  private:
    String serverUrl_;
}; 

#endif