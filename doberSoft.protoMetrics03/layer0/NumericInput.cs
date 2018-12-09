namespace doberSoft.protoMetrics03.layer0
{
    internal class NumericInput : AbstractInput<decimal>
    {
        public NumericInput(LogicIO logicIO, int i) : base(logicIO, i)
        {
        }

        public override decimal GetValue()
        {
            return _logicIO.GetDecimal(_i);
        }
    }
}