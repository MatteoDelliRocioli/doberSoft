using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.Rules;
using doberSoft.protoMetrics03.ScaleFunctions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace doberSoft.protoMetrics03.layer1
{
    class NumericSensor: AbstractSensor<decimal, decimal>
    {
        public NumericSensor(
            string name,
            int id)
                : base(name, id)
        {
            Console.WriteLine($"AnalogNoDetails_{Type}_created({Id})");
        }


        protected override void tmrPush_trig(object source, ElapsedEventArgs e)
        {
            // valuta le rules
            if (Extension.CheckHysteresis(GetCurValue(0), GetOldValue(0), Rules.HysteresisHi, Rules.HysteresisLo))
            {
                // appena c'è la condizione interrompiamo la verifica
                // e inviamo i dati
                BackUpInputs();
                SetValue(e);
            }
        }
    }
}
