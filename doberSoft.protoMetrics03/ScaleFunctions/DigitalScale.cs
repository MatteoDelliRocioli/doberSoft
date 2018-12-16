using doberSoft.protoMetrics03.layer1;
using System;

namespace doberSoft.protoMetrics03.ScaleFunctions
{
    class DigitalScale : IScale<bool, bool>
    {
        /// <summary>
        /// Mappa 1 a 1 l'ingresso (bool)input [0] con l'uscita (bool)Scale, 
        /// </summary>
        public DigitalScale()
        {
        }

        /// <summary>
        /// I parametri sono ininfluenti
        /// </summary>
        public IScaleParameters Parameters { get; private set; }

        /// <summary>
        /// Restituisce l'input[0]
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public bool Scale(IInput<bool>[] inputs)
        {
            //Console.WriteLine($"Scale digital { inputs[0].GetValue()}");
            return inputs[0].GetValue();
        }
    }
}
