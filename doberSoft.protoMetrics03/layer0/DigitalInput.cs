namespace doberSoft.protoMetrics03.layer0
{
    internal class DigitalInput : AbstractInput<bool>
    {
        public DigitalInput(LogicIO logicIO, int i) : base(logicIO, i)
        {
        }

        public override bool GetValue()
        {
            return _logicIO.GetBool(_i);
        }
    }
}