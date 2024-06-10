#include <numeric> 
#include "agregated_sensor_data_service.h";

AgregatedSensorDataService::AgregatedSensorDataService(int n) : readingsNumber(n) 
{ }

bool AgregatedSensorDataService::addHumidityReading(float value) {
  humidityData.push_back(value);
  if(humidityData.size() > readingsNumber) {
    return true;
  }
  return false;
}

bool AgregatedSensorDataService::addTempReading(float value) {
  tempData.push_back(value);
  if(tempData.size() > readingsNumber) {
    return true;
  }
  return false;
}

bool AgregatedSensorDataService::addWeightReading(float value) {
  weightData.push_back(value);
  if(weightData.size() > readingsNumber) {
    return true;
  }
  return false;
}

float AgregatedSensorDataService::countAverageSensorData(const std::vector<float>& values) {
  if (values.empty()) {
        return 0.0f;
    }

    float sum = std::accumulate(values.begin(), values.end(), 0.0f);
    float average = sum / values.size();
    return average;
}

float AgregatedSensorDataService::getAverageHumidity() {
  return countAverageSensorData(humidityData);
}

float AgregatedSensorDataService::getAverageTemp() {
  return countAverageSensorData(tempData);
}

float AgregatedSensorDataService::getAverageWeight() {
  return countAverageSensorData(weightData);
}

void AgregatedSensorDataService::resetHumidity() {
  humidityData.clear();
}

void AgregatedSensorDataService::resetTemp() {
  tempData.clear();
}

void AgregatedSensorDataService::resetWeight() {
  weightData.clear();
}

void AgregatedSensorDataService::reset() {
  resetHumidity();
  resetTemp();
  resetWeight();
}

void AgregatedSensorDataService::compareToCritical(const String& paramName, float highValue, float lowValue, float currentValue) {
    if(currentValue > highValue)
    {
      Serial.println(">> Current " + paramName + "(" + String(currentValue) + ") is higher than critical (" + String(highValue) + ") by "+ String(currentValue - highValue) + " <<");
    }
    if(currentValue < lowValue)
    {
      Serial.println(">> Current " + paramName + "(" + String(currentValue) + ") is lower than critical (" + String(lowValue) + ") by "+ String(lowValue - currentValue) +" <<");
    }

}