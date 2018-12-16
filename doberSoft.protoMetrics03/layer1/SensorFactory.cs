using doberSoft.protoMetrics03.ScaleFunctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.layer1
{
    public static class SensorFactory
    {
        private static Dictionary<Type, Type> registeredTypes = new Dictionary<System.Type, System.Type>();

        static SensorFactory()
        {
            registeredTypes.Add(typeof(TemperatureSensor), typeof(TemperatureSensor));
            registeredTypes.Add(typeof(StatusSensor), typeof(StatusSensor));
            registeredTypes.Add(typeof(AnalogSensor), typeof(AnalogSensor));
            registeredTypes.Add(typeof(PositionSensor), typeof(PositionSensor));
            registeredTypes.Add(typeof(Position), typeof(PositionSensor));
            registeredTypes.Add(typeof(bool), typeof(StatusSensor));
            registeredTypes.Add(typeof(int), typeof(AnalogSensor));
            registeredTypes.Add(typeof(decimal), typeof(NumericSensor));
        }

        public static ISensor CreateGeneric<T>(string name, int id)
        {
            var t = typeof(T);
            if (registeredTypes.ContainsKey(t) == false) throw new NotSupportedException();

            var typeToCreate = registeredTypes[t];
            return Activator.CreateInstance(typeToCreate, name, id) as ISensor;
        }

    }
}
