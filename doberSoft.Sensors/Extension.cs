using System;


namespace doberSoft.Sensors
{
    public static class Extension
    {
        public static bool CheckHysteresis(bool value, bool oldvalue, bool HysteresysHi, bool HysteresysLo)
        {
            //Console.WriteLine("CheckHysteresis bool");
            return value != oldvalue;
        }
        public static bool CheckHysteresis(int value, int oldvalue, int HysteresysHi, int HysteresysLo)
        {
            //Console.WriteLine("CheckHysteresis int");
            var diff = value - oldvalue;
            return diff < HysteresysLo || diff > HysteresysHi;
        }
        public static bool CheckHysteresis(decimal value, decimal oldvalue, decimal HysteresysHi, decimal HysteresysLo)
        {
            //Console.WriteLine("CheckHysteresis decimal");
            var diff = value - oldvalue;
            return diff < HysteresysLo || diff > HysteresysHi;
        }


        public static bool CheckHysteresis<T>(T value, T oldvalue, T HysteresysHi, T HysteresysLo)
        {
            Console.WriteLine("CheckHysteresis generic");

            // https://www.codeproject.com/Questions/1103617/Generics-and-the-arithmetic-problem-Csharp
            // https://stackoverflow.com/questions/10951392/implementing-arithmetic-in-generics
            switch (typeof(T).Name)
            {
                case "Int32":
                    var diff1 = (int)(object)value - (int)(object)oldvalue;
                    return diff1 < (int)(object)HysteresysLo || diff1 > (int)(object)HysteresysHi;
                case "Double":
                    var diff2 = (double)(object)value - (double)(object)oldvalue;
                    return diff2 < (double)(object)HysteresysLo || diff2 > (double)(object)HysteresysHi;
                case "Decimal":
                    var diff3 = (decimal)(object)value - (decimal)(object)oldvalue;
                    return diff3 < (decimal)(object)HysteresysLo || diff3 > (decimal)(object)HysteresysHi;
                case "Long":
                    var diff4 = (long)(object)value - (long)(object)oldvalue;
                    return diff4 < (long)(object)HysteresysLo || diff4 > (long)(object)HysteresysHi;
                case "String":
                    return ((string)(object)value).CompareTo((string)(object)value)!=0;
                default:
                    return true;
            }

        }
    }
}
