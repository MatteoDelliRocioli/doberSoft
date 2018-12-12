using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.layer1
{
    public static class Extension
    {
        public static bool CheckHysteresis(this bool value, bool oldvalue, bool HysteresysHi, bool HysteresysLo)
        {

            return value != oldvalue;
        }
        public static bool CheckHysteresis(this int value, int oldvalue, int HysteresysHi, int HysteresysLo)
        {
            bool isMinor = (value - oldvalue) < HysteresysLo;
            bool isMajor = (value - oldvalue) > HysteresysHi;
            return isMinor || isMajor;
        }
        public static bool CheckHysteresis(this decimal value, decimal oldvalue, decimal HysteresysHi, decimal HysteresysLo)
        {
            bool isMinor = (value - oldvalue) < HysteresysLo;
            bool isMajor = (value - oldvalue) > HysteresysHi;
            return isMinor || isMajor;
        }
    }
}
