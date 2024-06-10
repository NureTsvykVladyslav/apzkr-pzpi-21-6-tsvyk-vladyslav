#ifndef CONFIG_STRUCTURE_H
#define CONFIG_STRUCTURE_H

struct Config {
  String hiveId;
  String hubId;
  String humiditySensorId;
  String weightSensorId;
  String tempSensorId;
  String ssid;
  String password;
  float criticalHumidityHigh;
  float criticalHumidityLow;
  float criticalTempHigh;
  float criticalTempLow;
  String serverUrl;
};

#endif
