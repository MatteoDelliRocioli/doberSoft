using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.Buffer
{
    public interface IBufferData
    {
        int Priority { get; }
        IDatapacket Data { get; set; }
    }
    public interface IDatapacket
    {
        string Topic { get; }
    }
    public interface IDataPacket<T>:IDatapacket
    {
        T Payload{ get; }
    }
}
