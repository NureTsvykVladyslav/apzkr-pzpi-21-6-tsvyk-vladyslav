#ifndef CONFIG_MANAGER_H
#define CONFIG_MANAGER_H

#include <ArduinoJson.h>
#include "config_structure.h"

class ConfigManager {
public:
  bool loadFromFile(const char* filename);
  bool loadFromServer(const char* serverUrl);
  const Config& getConfig() const;
  void printCurrentConfig();

private:
  Config config_;
};


#endif