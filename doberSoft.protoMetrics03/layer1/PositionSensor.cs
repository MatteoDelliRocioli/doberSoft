using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.Rules;
using doberSoft.protoMetrics03.ScaleFunctions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace doberSoft.protoMetrics03.layer1
{
    public class PositionSensor : AbstractSensor<decimal, Position>
    {
        public PositionSensor(
            string name,
            int id,
            IScale<decimal, Position> scaleFunction,
            IRules<decimal> rules,
            IInput<decimal> input1,
            IInput<decimal> input2)
                : base(name, id, scaleFunction, rules)
        {
            InputAdd(input1);
            InputAdd(input2);
        }

        protected override void tmrPush_trig(object source, ElapsedEventArgs e)
        {
            // valuta le rules
            decimal hHi = Rules.HysteresisHi;
            decimal hLo = Rules.HysteresisLo;

            for (int i = 0; i < InputCount; i++)
            {
                //Console.WriteLine($"position_{Name} push({GetCurValue(i)}) - ({GetOldValue(i)}) = { GetCurValue(i) - GetOldValue(i)} > {hHi}, < {hLo}");
                var result = GetCurValue(i) - GetOldValue(i);
                if (result > hHi || result < hLo)
                {
                    //Console.WriteLine("   fire");
                    BackUpInputs();
                    SetValue(e);
                    break;
                }
            }
        }
    }
}
