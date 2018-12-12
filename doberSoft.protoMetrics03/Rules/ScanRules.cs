using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.Rules
{
    public class ScanRules<T> : IRules<T> 
    {
        public ScanModeConstants ScanMode { get; set; }
        public double ScanInterval { get; set; }
        public T HysteresisHi { get; set; }
        public T HysteresisLo { get; set; }
    }
}
