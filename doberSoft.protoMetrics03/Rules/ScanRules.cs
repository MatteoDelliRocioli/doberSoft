using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.Rules
{
    public class ScanRules : IRules
    {
        public ScanModeConstants ScanMode { get; set; }
        public double ScanInterval { get; set; }
    }
}
