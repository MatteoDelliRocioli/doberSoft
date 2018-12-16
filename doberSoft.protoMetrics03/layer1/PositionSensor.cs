using doberSoft.protoMetrics03.ScaleFunctions;
using System;
using System.Timers;

namespace doberSoft.protoMetrics03.layer1
{
    public class PositionSensor : AbstractSensor<decimal, Position>
    {
        public PositionSensor()
        {                
        }

        public PositionSensor(
            string name,
            int id)
                : base(name, id)
        {
            Console.WriteLine($"PositionNoDetails_{Type}_created({Id})");
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
        }
    }
}
