﻿using doberSoft.protoMetrics03.layer0;
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
        public TemperatureSensor()        {

        }
        public TemperatureSensor(
            string name,
            int id)
                : base(name, id)
        {
            Console.WriteLine($"TemperatureNoDetails_{Type}_created({Id})");
        }
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
