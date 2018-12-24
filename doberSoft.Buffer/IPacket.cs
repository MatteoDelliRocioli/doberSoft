using System;
using System.Collections.Generic;
using System.Text;

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
        string Topic { get; set; }
    }
    public interface IPacket<T>: IPacket
    {
        T Payload { get; }
    }
    public interface IBufferPacket<T> : IPacket<T>, IPacketKey
    {
    }
}
