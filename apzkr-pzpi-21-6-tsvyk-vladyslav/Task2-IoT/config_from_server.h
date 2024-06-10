#ifndef CONFIG_FROM_SERVER_H
#define CONFIG_FROM_SERVER_H

struct SensorData {
    float criticalHumidity;
    float criticalTemp;
    int humiditySensorId;
    int weightSensorId;
    int tempSensorId;
    int hubStationId;
};

#endif