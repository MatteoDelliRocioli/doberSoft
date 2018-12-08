using doberSoft.protoMetrics02.Utils;
using System;

namespace doberSoft.protoMetrics02.Sensors
{
    // https://github.com/Microsoft/Windows-iotcore-samples/tree/master/Samples/TempForceSensor/CS
    // https://blogs.msdn.microsoft.com/uk_faculty_connection/2018/04/30/temperature-sensing-and-control-using-raspberry-pi/
    // https://stackoverflow.com/questions/5842339/how-to-trigger-event-when-a-variables-value-is-changed
    // https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/ms743695(v=vs.100) 


    class GenericSensor<Tin, Tout> :ISensor<Tin, Tout>
    {
        private Tin Value;
        int MappedInput;
        private Action<GenericSensor<Tin, Tout>, int, Tin> Updater;

        public event EventHandler<SensorEventArgs<Tin,Tout>> ValueChanged;

        public GenericSensor(
            string name,
            int id,
            IScale<Tin, Tout> scaleFunction,
            int mappedInput,
            Action<GenericSensor<Tin, Tout>,int, Tin> updater)
        {
            ScaleFunction = scaleFunction;
            Id = id;
            Name = name;
            Updater = updater;
            MappedInput = mappedInput;
        }

        public IScale<Tin, Tout> ScaleFunction { get; private set; }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public void SetValue(Tin value)
        {
            Value = value;
            ValueChanged?.Invoke(this, new SensorEventArgs<Tin,Tout>());
        }
        public Tout GetValue()
        {
            return ScaleFunction.Scale(Value);
        }

        private void Update()
        {
            Updater.Invoke(this, MappedInput, Value);
        }

        public string ToJson()
        {
            string json = $"{{";
            json = $"{json},\"type\":\"{Name}\"";
            json = $"{json},\"id\":{Id}\"";
            // i generici sono castati come object ed espongono solo le proprietà
            // e i metodi di object
            //json = $"{json},\"value\":{sensor.GetValue().ToString("0.00")}\"";
            json = $"{json},\"value\":{GetValue().ToString()}\"";
            json = $"{json},\"timestamp\":{Strings.GetTimestamp(DateTime.Now) }\"";
            json = $"{json}}}";

            return json;
        }
    }
}
