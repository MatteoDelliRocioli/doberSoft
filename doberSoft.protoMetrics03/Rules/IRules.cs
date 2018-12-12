using System;

namespace doberSoft.protoMetrics03.Rules
{
    public interface IRules<T> 
    {
        ScanModeConstants ScanMode { get; set; }
        double ScanInterval { get; }
        T HysteresisHi { get; }
        T HysteresisLo { get; }
    }
    public enum ScanModeConstants
    {
        PushMode,
        PollMode
    }
}
