using System;


namespace doberSoft.protoMetrics03.layer1
{
    public static class Extension
    {
        public static bool CheckHysteresis( bool value, bool oldvalue, bool HysteresysHi, bool HysteresysLo)
        {
            //Console.WriteLine("CheckHysteresis bool");
            return value != oldvalue;
        }
        public static bool CheckHysteresis( int value, int oldvalue, int HysteresysHi, int HysteresysLo)
        {
            //Console.WriteLine("CheckHysteresis int");
            var diff = value - oldvalue;
            return diff < HysteresysLo || diff > HysteresysHi;
        }
        public static bool CheckHysteresis( decimal value, decimal oldvalue, decimal HysteresysHi, decimal HysteresysLo)
        {
            //Console.WriteLine("CheckHysteresis decimal");
            var diff = value - oldvalue;
            return diff < HysteresysLo || diff > HysteresysHi;
        }
        public static bool CheckHysteresis<T>( T value, T oldvalue, T HysteresysHi, T HysteresysLo)
        {
            Console.WriteLine("CheckHysteresis generic");
            try
            {
                var diff = (dynamic)value - oldvalue;
                return diff < HysteresysLo || diff > HysteresysHi;
            } catch (Exception e)
            {
                return true;
            }
        }
    }
}
