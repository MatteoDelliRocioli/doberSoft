namespace doberSoft.protoMetrics02.Sensors
{
    class LinearScale : IScale<int, decimal>
    {

        public LinearScale(int minIn, int maxIn, decimal minOut, decimal maxOut)
        {
            Parameters = new LinearScaleParameters()
            {
                MinIn = minIn,
                MaxIn = maxIn,
                MinOut = minOut,
                MaxOut = maxOut
            };
        }

        public IScaleParameters Parameters { get; private set; }

        public decimal Scale(int Value)
        {
  
            //Console.WriteLine($"    Scale ({Value - Parameters.MinIn}) / ({Parameters.MaxIn - Parameters.MinIn}) * ({Parameters.MaxOut - Parameters.MinOut}) + {Parameters.MinOut} = {(Value - Parameters.MinIn) / (Parameters.MaxIn - Parameters.MinIn) * (Parameters.MaxOut - Parameters.MinOut) + Parameters.MinOut}");

            return (Value - (decimal)Parameters.MinIn) / (Parameters.MaxIn - Parameters.MinIn) * (Parameters.MaxOut - Parameters.MinOut) + Parameters.MinOut;
        }
        }
}
