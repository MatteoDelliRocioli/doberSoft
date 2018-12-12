using doberSoft.protoMetrics03.layer0;

namespace doberSoft.protoMetrics03.ScaleFunctions
{
    class PositionScale : IScale<decimal, Position>
    {
        public PositionScale() : this(100)
        {

        }
        public PositionScale(int scaleFactor)
        {
            Parameters = new ScaleParameters();
            Parameters.MaxOut = scaleFactor;
        }

        public IScaleParameters Parameters { get; private set; }

        public Position Scale(IInput<decimal>[] inputs)
        {
            return new Position(inputs[0].GetValue()/ Parameters.MaxOut, inputs[1].GetValue() / Parameters.MaxOut);
        }

    }
}
