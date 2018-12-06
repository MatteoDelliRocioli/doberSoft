using System;

namespace doberSoft.protoMetrics02.Sensors
{

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

        public void Update()
        {
            Updater.Invoke(this, MappedInput, Value);
        }

    }
}
