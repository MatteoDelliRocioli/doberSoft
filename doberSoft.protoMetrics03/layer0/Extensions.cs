using doberSoft.protoMetrics03.layer1;
using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.layer0
{
    static class Extensions
    {
        public static IInput<bool> GetDigitalInput(this LogicIO parent, int i)
        {
            return new DigitalInput(parent, i);
        }

        public static IInput<int> GetAnalogInput(this LogicIO parent, int i)
        {
            return new AnalogInput(parent, i);
        }

        public static IInput<decimal> GetNumericInput(this LogicIO parent, int i)
        {
            return new NumericInput(parent, i);
        }

        public static IInput<string> GetStringInput(this LogicIO parent, int i)
        {
            return new StringInput(parent, i);
        }
    }
}
