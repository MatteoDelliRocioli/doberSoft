using doberSoft.protoMetrics03.layer0;

namespace doberSoft.protoMetrics03.ScaleFunctions
{
    class DigitalScale : IScale<bool, bool>
    {
        // i parametri sono ininfluenti
        public DigitalScale()
        {
        }

        public IScaleParameters Parameters { get; private set; }

        public bool Scale(IInput<bool>[] inputs)
        {
            return inputs[0].GetValue();
        }
    }
}
