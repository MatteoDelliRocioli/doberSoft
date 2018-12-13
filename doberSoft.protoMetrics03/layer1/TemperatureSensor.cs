using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.Rules;
using doberSoft.protoMetrics03.ScaleFunctions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace doberSoft.protoMetrics03.layer1
{
    class TemperatureSensor: AbstractSensor<int, decimal>
    {
        public TemperatureSensor(
            string name,
            int id,
            IScale<int, decimal> scaleFunction,
            IRules<int> rules,
            IInput<int> input)
                : base(name, id, scaleFunction, rules)
        {
            Console.WriteLine($"Temperature_{Type}_created({Id})");
            InputAdd(input);;
        }

        protected override void tmrPush_trig(object source, ElapsedEventArgs e)
        {
            // valuta le rules
            int hHi = Rules.HysteresisHi;
            int hLo = Rules.HysteresisLo;

            var result = GetCurValue(0) - GetOldValue(0);
            if (result > hHi || result < hLo)
            {
                BackUpInputs();
                SetValue(e);
            }
        }
    }
}
