using System;

namespace doberSoft.Buffers
{
    public interface IPacketKey : IComparable<IPacketKey>
    {
        int Priority { get; }
        DateTime TimeStamp { get; }
    }
    public interface IPacket
    {
        IPacketKey Key { get; }
        int StageId { get; set; }
        string Topic { get; set; }

    }
    public interface IPacket<TPayload> : IPacket
    {
        TPayload Payload { get; }
    }
    public interface IBufferPacket<TPayload> : IPacket<TPayload>, IPacketKey
    {
    }
}
