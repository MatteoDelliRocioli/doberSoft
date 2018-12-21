using doberSoft.Sensors.Core;
using doberSoft.Sensors.Core.Rules;
using doberSoft.Sensors.Core.ScaleFunctions;
using System;
using System.Timers;

namespace doberSoft.Sensors
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Tin"></typeparam>
    /// <typeparam name="Tout"></typeparam>
    public class GenericSensor<Tin, Tout> : AbstractSensor<Tin, Tout> 
    {
        public GenericSensor(
            string name,
            int id)
                : base(name, id)
        {
            Console.WriteLine($"GenericNoDetails_{Type}_created({Id})");
        }


        /// <summary>
        /// Restituiesce un sensore generico mappato su singolo input
        /// </summary>
        /// <param name="name">Etichetta assegnata al sensore</param>
        /// <param name="id">Identificativo del sensore</param>
        /// <param name="scaleFunction"></param>
        /// <param name="rules"></param>
        /// <param name="input"></param>
        public GenericSensor(
            string name,
            int id,
            IScale<Tin, Tout> scaleFunction,
            IRules<Tin> rules,
            IInput<Tin> input)
                : base(name, id, scaleFunction, rules)
        {
            Console.WriteLine($"Generic1_{Type}_created({Id})");
            _inputsList.Add(input);
        }
        /// <summary>
        /// Restituisce un sensore generico mappato su 2 inputs, tipicamente un sensore di posizione che legge 2 campi dal bus. 
        /// Tin: decimal; 
        /// Tout: position
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="scaleFunction"></param>
        /// <param name="rules"></param>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        public GenericSensor(
            string name,
            int id,
            IScale<Tin, Tout> scaleFunction,
            IRules<Tin> rules,
            IInput<Tin> input1,
            IInput<Tin> input2)
                : base(name, id, scaleFunction, rules)
        {
            Console.WriteLine($"Generic2_{Type}_created({Id})");
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
        }
    }


}
