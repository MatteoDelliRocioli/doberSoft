using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.Buffers
{
    public interface IBuffer<T>
    {
        IBufferPacket<T> Push(int priority, DateTime timeStamp, T payload);
        IEnumerable<IBufferPacket<T>> Get(out int id, int MaxCount = -1);
        void Confirm(int id);
        void Cancel(int id);
        bool NotEmpty { get; }
        bool Empty { get; }
        int Length { get; }
    }
}
