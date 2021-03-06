﻿using doberSoft.Sensors.Core;
using doberSoft.Sensors.Core.ScaleFunctions;

namespace doberSoft.Sensors.ScaleFunctions
{
    public class PositionScale : IScale<decimal, Position>
    {
        public PositionScale() : this(0.01M)
        {

        }
        public PositionScale(decimal scaleFactor)
        {
            Parameters = new ScaleParameters()
            {
                MinIn = 0,
                MaxIn = 1,
                MinOut = 0,
                MaxOut = scaleFactor
            };
        }

        public IScaleParameters Parameters { get; private set; }

        public Position Scale(IInput<decimal>[] inputs)
        {

            return new Position(inputs[0].GetValue() * Parameters.MaxOut, inputs[1].GetValue() * Parameters.MaxOut);
        }

    }
}
