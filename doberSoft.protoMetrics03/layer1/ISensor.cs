using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.ScaleFunctions;
using doberSoft.protoMetrics03.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.layer1
{
    public interface ISensor<Tin, Tout>
    {

        event EventHandler<SensorEventArgs<Tin, Tout>> ValueChanged;

        int Id { get; }
        string Name { get; }
        IRules Rules { get; }
        Tout GetValue();
        IScale<Tin, Tout> ScaleFunction { get; }
        string ToJson();
        void On();
        void Off();
    }
}
