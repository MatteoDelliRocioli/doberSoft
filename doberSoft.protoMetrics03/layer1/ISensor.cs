using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.ScaleFunctions;
using doberSoft.protoMetrics03.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.layer1
{
    public interface ISensor
    {
        event EventHandler<SensorEventArgs> ValueChanged;

        int Id { get; }
        string Name { get; }
        string ToJson();
        void On();
        void Off();
    }
    public interface ISensor<Tin, Tout>: ISensor
    {

        //event EventHandler<SensorEventArgs<Tin, Tout>> ValueChanged;

        IRules<Tin> Rules { get; }
        Tout GetValue();
        IScale<Tin, Tout> ScaleFunction { get; }
        //Type GetInputType { get; }
        //Type GetOutputType { get; }
        //void Update(); TODO: aggiornamento su trigger esterno
    }
}
