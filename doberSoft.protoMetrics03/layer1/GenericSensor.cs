﻿using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.Rules;
using doberSoft.protoMetrics03.ScaleFunctions;
using System;
using System.Timers;

namespace doberSoft.protoMetrics03.layer1
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Tin"></typeparam>
    /// <typeparam name="Tout"></typeparam>
    public class GenericSensor<Tin, Tout> : AbstractSensor<Tin, Tout> 
    {

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
            _inputs.Add(input);
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
    }


}
