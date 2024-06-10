#include "time_util.h"

String getUtcTime() {
  time_t now = time(nullptr);
  struct tm timeinfo;
  gmtime_r(&now, &timeinfo);
  char formattedTime[24];
  strftime(formattedTime, sizeof(formattedTime), "%FT%TZ", &timeinfo);
  return String(formattedTime);
}