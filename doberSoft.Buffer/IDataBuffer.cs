using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.Buffer
{
    public interface IDataBuffer
    {
        void Push(IBufferData data);
        IBufferData[] Get(out int id, int MaxCount = -1);
        void Confirm(int id);
        void Cancel(int id);
        bool NotEmpty { get; }
        bool Empty { get; }
        int Length { get; }
    }
}
