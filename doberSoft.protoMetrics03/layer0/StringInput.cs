namespace doberSoft.protoMetrics03.layer0
{
    internal class StringInput : AbstractInput<string>
    {
        public StringInput(LogicIO logicIO, int i) : base(logicIO, i)
        {
        }

        public override string GetValue()
        {
            return _logicIO.GetString(_i);
        }
    }
}