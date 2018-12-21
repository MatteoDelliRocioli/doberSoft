using doberSoft.Sensors.Core;
using doberSoft.Sensors.ScaleFunctions;
using System;
using System.Collections.Generic;

namespace doberSoft.Sensors
{
    public static class SensorFactory
    {
        private static Dictionary<Type, Type> registeredTypes = new Dictionary<System.Type, System.Type>();
        public static void Register<TKey, TValue>()
        {
            registeredTypes.Add(typeof(TKey), typeof(TValue));
        }
        static SensorFactory()
        {
            Register<NumericSensor,NumericSensor>();
            Register<TemperatureSensor, TemperatureSensor>();
            Register<StatusSensor, StatusSensor>();
            Register<AnalogSensor, AnalogSensor>();
            Register<PositionSensor, PositionSensor>();
            Register<Position, PositionSensor>();
            Register<bool, StatusSensor>();
            Register<int, AnalogSensor>();
            Register<decimal, NumericSensor>();
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
