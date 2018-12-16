using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.Rules;
using doberSoft.protoMetrics03.ScaleFunctions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace doberSoft.protoMetrics03.layer1
{
    class AnalogSensor: AbstractSensor<int, decimal>
    {
        public AnalogSensor(
            string name,
            int id)
                : base(name, id)
        {
            Console.WriteLine($"AnalogNoDetails_{Type}_created({Id})");
        }


        public AnalogSensor(
            string name,
            int id,
            IScale<int, decimal> scaleFunction,
            IRules<int> rules,
            IInput<int> input)
                : base(name, id, scaleFunction, rules)
        {
            Console.WriteLine($"Analog_{Type}_created({Id})");
            InputAdd(input); ;
        }

        public AnalogSensor(
            string name,
            int id,
            int bitResolution,
            decimal minOut,
            decimal maxOut,
            ScanModeConstants scanMode,
            double scanInterval,
            IInput<int> input)
                : this(name, id, bitResolution, minOut, maxOut, scanMode, scanInterval, 0, 0, input)
        {
        }

        public AnalogSensor(
            string name,
            int id,
            int bitResolution,
            decimal minOut,
            decimal maxOut,
            ScanModeConstants scanMode,
            double scanInterval,
            int hystersisLo,
            int hystersisHi,
            IInput<int> input)
                : this(name, id,
                      new LinearScale(0, (int)Math.Pow(2, bitResolution), minOut, maxOut),
                      new ScanRules<int>
                      {
                          ScanMode = scanMode,
                          ScanInterval = scanInterval,
                          HysteresisLo = hystersisLo,
                          HysteresisHi = hystersisHi
                      }, input)
        {
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
