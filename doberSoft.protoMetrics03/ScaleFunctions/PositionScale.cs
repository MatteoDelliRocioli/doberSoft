using doberSoft.protoMetrics03.layer0;

namespace doberSoft.protoMetrics03.ScaleFunctions
{
    class PositionScale : IScale<decimal, Position>
    {
        public PositionScale():this(1)
        {

        }
        public PositionScale(int options)
        {
            /* ****************
             * options determina se i gradi vengono formattati in
             * decimale,
             * sessagesimale
             * altro?
             */
        }

        public IScaleParameters Parameters { get; private set; }

        public Position Scale(IInput<decimal>[] inputs)
        {
            return new Position(inputs[0].GetValue(), inputs[1].GetValue());
        }

    }
}
