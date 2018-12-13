using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.layer0
{
    public interface IInput
    {
        T GetTValue<T>();
    }
    public interface IInput<T> //: IInput
    {
        T GetValue();
    }
}
