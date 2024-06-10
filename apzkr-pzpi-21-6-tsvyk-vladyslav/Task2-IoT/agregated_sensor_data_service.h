#ifndef AGREGATED_SENSOR_DATA_SERVICE_H
#define AGREGATED_SENSOR_DATA_SERVICE_H

#include <Arduino.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include <WiFi.h>
#include <vector>

/**
 * @brief Class for grouping all sensor data into arrays and.
 */
class AgregatedSensorDataService {
  public:
    AgregatedSensorDataService(int n);

    // returns true if vector is full;
    bool addHumidityReading(float value);
    bool addTempReading(float value);
    bool addWeightReading(float value);

    float getAverageHumidity();
    float getAverageTemp();
    float getAverageWeight();
    float countAverageSensorData(const std::vector<float>& values);
    void reset();
    void resetHumidity();
    void resetTemp();
    void resetWeight();
    void compareToCritical(const String& paramName, float highValue, float lowValue, float currentValue);
  private:
    const int readingsNumber;
    std::vector<float> humidityData;
    std::vector<float> tempData;
    std::vector<float> weightData;
};

#endif