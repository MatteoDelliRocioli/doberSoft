using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.Rules;
using doberSoft.protoMetrics03.ScaleFunctions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace doberSoft.protoMetrics03.layer1
{
    class StatusSensor : AbstractSensor<bool, bool>
    {

        /// <summary>
        /// Restituisce un sensore digitale mappato su singolo input. Non usa scalatura. Non usa isteresi
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="ScanMode"></param>
        /// <param name="ScanInterval"></param>
        /// <param name="input"></param>
        public StatusSensor(
            string name,
            int id,
            ScanModeConstants ScanMode,
            double ScanInterval,
            IInput<bool> input)
                : base(name, id, new ScanRules<bool>
                {
                    ScanMode = ScanMode,
                    ScanInterval = ScanInterval
                })
        {
            Console.WriteLine($"Status_{Type}_created({Id})");
            InputAdd(input);
        }
        /// <summary>
        /// Scansiona l'ingresso con la frequenza impostata e invia i dati solo lo stato è diverso dal precedente
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected override void tmrPush_trig(object source, ElapsedEventArgs e)
        {
            //Console.WriteLine($"Digital_{Name} push({GetCurValue(0)}) != ({GetOldValue(0)})");
            if (GetValue() == GetOldValue(0))
                return;
            //Console.WriteLine("   fire");
            BackUpInputs();
            SetValue(e);
        }
        /// <summary>
        /// Restituisce direttamente il contenuto di inputs[0], senza scalature
        /// </summary>
        /// <returns></returns>
        public override bool GetValue()
        {
            return GetCurValue(0);
        }
    }
}
