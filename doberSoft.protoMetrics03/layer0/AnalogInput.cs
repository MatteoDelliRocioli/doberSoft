namespace doberSoft.protoMetrics03.layer0
{
    internal class AnalogInput : AbstractInput<int>
    {
        public AnalogInput(LogicIO logicIO, int i) : base(logicIO, i)
        {
        }

        public override int GetValue()
        {
            return _logicIO.GetInt(_i);
        }
    }
}