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
            int id)
                : base(name, id)
        {
            Console.WriteLine($"PositionNoDetails_{Type}_created({Id})");
        }
        public PositionSensor(
            string name,
            int id,
            IScale<decimal, Position> scaleFunction,
            IRules<decimal> rules,
            IInput<decimal> input1,
            IInput<decimal> input2)
                : base(name, id, scaleFunction, rules)
        {
            Console.WriteLine($"Position_{Type}_created({Id})");
            InputAdd(input1);
            InputAdd(input2);
        }

        protected override void tmrPush_trig(object source, ElapsedEventArgs e)
        {
            // valuta le rules
            for (int i = 0; i < _inputsList.Count; i++)
            {
                if (Extension.CheckHysteresis(GetCurValue(i), GetOldValue(i), Rules.HysteresisHi, Rules.HysteresisLo))
                {
                    // appena c'è la condizione interrompiamo la verifica
                    // e inviamo i dati
                    BackUpInputs();
                    SetValue(e);
                    break;
                }
            }
            //// valuta le rules
            //decimal hHi = Rules.HysteresisHi;
            //decimal hLo = Rules.HysteresisLo;
            //for (int i = 0; i < InputCount; i++)
            //{
            //    var result = GetCurValue(i) - GetOldValue(i);
            //    if (result > hHi || result < hLo)
            //    {
            //        BackUpInputs();
            //        SetValue(e);
            //        break;
            //    }
            //}
        }
    }
}
