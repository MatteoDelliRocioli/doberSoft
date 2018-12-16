
using System;
using System.Timers;

namespace doberSoft.protoMetrics03.layer1
{
    class StatusSensor : AbstractSensor<bool, bool>
    {
        public StatusSensor()
        {
        }

        public StatusSensor(
            string name,
            int id)
                : base(name, id)
        {
            Console.WriteLine($"StatusNoDetails_{Type}_created({Id})");
        }
        /// <summary>
        /// Scansiona l'ingresso con la frequenza impostata e invia i dati solo lo stato è diverso dal precedente
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
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
