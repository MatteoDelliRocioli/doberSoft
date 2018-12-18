using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.Buffer
{
    public interface IDataBuffer
    {
        void Push(IBufferData data, int priority);
        IBufferData[] Get(out int key, int MaxCount = -1);
        void Confirm(int key);
        void Cancel(int key);
        bool NotEmpty { get; }
        bool Empty { get; }
        int Length { get; }
    }
}
