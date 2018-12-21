using doberSoft.Sensors.Core.Rules;

namespace doberSoft.Sensors.Rules
{
    public class ScanRules<T> : IRules<T> 
    {
        public ScanModeConstants ScanMode { get; set; }
        public double ScanInterval { get; set; }
        public T HysteresisHi { get; set; }
        public T HysteresisLo { get; set; }
    }
}
