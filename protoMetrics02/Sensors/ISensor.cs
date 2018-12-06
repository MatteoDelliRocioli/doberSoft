using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics02.Sensors
{
    interface ISensor<Tin, Tout>
    {
        int Id { get; }
        string Name { get; }
        Tout GetValue();
        IScale<Tin, Tout> ScaleFunction { get; }
        void Update();
    }
}
