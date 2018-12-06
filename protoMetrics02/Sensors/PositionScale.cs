namespace doberSoft.protoMetrics02.Sensors
{
    class PositionScale : IScale<Position, Position>
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

        public Position Scale(Position Value)
        {
            return Value;
        }

    }
}
