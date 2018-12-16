using doberSoft.protoMetrics03.Rules;
using doberSoft.protoMetrics03.ScaleFunctions;
using System.Collections;
using System.Collections.Generic;

// https://stackoverflow.com/questions/39386586/c-sharp-generic-interface-and-factory-pattern
// https://codereview.stackexchange.com/questions/8307/implementing-factory-design-pattern-with-generics

namespace doberSoft.protoMetrics03.layer1
{
    interface ISensorBuilder
    {
        ISensorBuilder CreateSensorOfType<T>(string name, int id) where T : ISensor, new();
        ISensorBuilder CreateSensorFordata<T>(string name, int id) where T : new();
        ISensorBuilder WithScaleFunction(IScale scaleFunction);
        ISensorBuilder WithRules<T>(ScanModeConstants scanMode, double scanInterval);
        ISensorBuilder WithRules<T>(ScanModeConstants scanMode, double scanInterval, T hystersisLo, T hystersisHi);
        ISensorBuilder InputAdd<T>(IInput<T> input);
        ISensor Build();
    }
    class SensorsList : ISensorBuilder, IEnumerable<ISensor>
    {
        List<ISensor> _list = new List<ISensor>();
        ISensor _sensor;

        public SensorsList()
        {

        }
        public ISensorBuilder CreateSensorOfType<T>(string name, int id) where T : ISensor, new()
        {
            _sensor = SensorFactory.CreateGeneric<T>(name, id);
            return this;
        }

        public ISensorBuilder CreateSensorFordata<T>(string name, int id) where T : new()
        {
            _sensor = SensorFactory.CreateGeneric<T>(name, id);
            return this;
        }

        public ISensorBuilder WithScaleFunction(IScale scaleFunction)
        {
            _sensor.SetScale(scaleFunction);
            return this;
        }
        public ISensorBuilder WithRules<T>(ScanModeConstants scanMode, double scanInterval)
        {
            _sensor.SetRules(new ScanRules<T>
            {
                ScanMode = scanMode,
                ScanInterval = scanInterval
            });

            return this;
        }

        public ISensorBuilder WithRules<T>(ScanModeConstants scanMode, double scanInterval, T hystersisLo, T hystersisHi)
        {
            _sensor.SetRules(new ScanRules<T>
            {
                ScanMode = scanMode,
                ScanInterval = scanInterval,
                HysteresisLo = hystersisLo,
                HysteresisHi = hystersisHi
            });

            return this;
        }

        public ISensorBuilder InputAdd<T>(IInput<T> input)
        {
            _sensor.InputAdd(input);
            return this;
        }

        public ISensor Build()
        {
            _list.Add(_sensor);       
            return _sensor;
        }

        public ISensor this[int index]
        {
            get { return _list[index]; }
        }

        public IEnumerator<ISensor> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
