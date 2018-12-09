namespace doberSoft.protoMetrics03.Rules
{
    public interface IRules
    {
        ScanModeConstants ScanMode { get; set; }
        double ScanInterval { get; }
    }
    public enum ScanModeConstants
    {
        PushMode,
        PollMode
    }
}
