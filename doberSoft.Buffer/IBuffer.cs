using System;
using System.Collections.Generic;

namespace doberSoft.Buffers
{
    public interface IBuffer<T>
    {
        IBufferPacket<T> Push(int priority, DateTime timeStamp, T payload);
        IEnumerable<IBufferPacket<T>> Get(out int stageId, int maxCount = -1);
        void Confirm(int stageId);
        void Cancel(int stageId);
        bool NotEmpty { get; }
        bool Empty { get; }
        int Length { get; }
    }
}
