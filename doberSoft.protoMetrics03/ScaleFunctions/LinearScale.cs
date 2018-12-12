using doberSoft.protoMetrics03.layer0;

namespace doberSoft.protoMetrics03.ScaleFunctions
{
    class LinearScale : IScale<int, decimal>
    {

        public LinearScale(int minIn, int maxIn, decimal minOut, decimal maxOut)
        {
            Parameters = new ScaleParameters()
            {
                MinIn = minIn,
                MaxIn = maxIn,
                MinOut = minOut,
                MaxOut = maxOut
            };
        }

        public IScaleParameters Parameters { get; private set; }

        public decimal Scale(IInput<int>[] inputs)
        {
            decimal k = (Parameters.MaxOut - Parameters.MinOut) / (Parameters.MaxIn - Parameters.MinIn);
            return (inputs[0].GetValue() - Parameters.MinIn) * k  + Parameters.MinOut;
        }
    }
}
