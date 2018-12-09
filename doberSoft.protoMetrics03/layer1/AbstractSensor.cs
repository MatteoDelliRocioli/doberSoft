using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.ScaleFunctions;
using doberSoft.protoMetrics03.Rules;
using System;
using System.Collections.Generic;
using System.Text;

using System.Timers;
namespace doberSoft.protoMetrics03.layer1
{
    public abstract class AbstractSensor<Tin, Tout> : ISensor<Tin, Tout> //where Tin : new() where Tout : new()
    {
        Timer timer;
        protected IInput<Tin>[] _inputs;
        public event EventHandler<SensorEventArgs<Tin, Tout>> ValueChanged;

        protected AbstractSensor(
            string name,
            int id,
            IScale<Tin, Tout> scaleFunction,
            IRules rules)
        {
            Id = id;
            Name = name;
            ScaleFunction = scaleFunction;
            Rules = rules;
        }

        public string Name { get; private set; }
        public int Id { get; private set; }

        public IRules Rules { get; private set; }

        public IScale<Tin, Tout> ScaleFunction { get; private set; }
        public Tout GetValue()
        {
            var a = _inputs;
            return ScaleFunction.Scale(_inputs);
        }

        public void Off()
        {
            timer.Enabled = false;
            timer.AutoReset = false;
            timer.Stop();
            timer.Dispose();
        }

        public void On()
        {
            // iniziaizziamo il loop di osservazione dell'input
            timer = new Timer(Rules.ScanInterval);
            if (Rules.ScanMode == ScanModeConstants.PushMode)
            {
                timer.Elapsed += tmrPush_trig;
            }
            else if (Rules.ScanMode == ScanModeConstants.PollMode)
            {
                timer.Elapsed += tmrPoll_trig;
            }
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();
        }

        public virtual string ToJson()
        {
            return $"ciao da {Name}";
        }
        private void tmrPoll_trig(Object source, ElapsedEventArgs e)
        {
            SetValue();
        }
        private void tmrPush_trig(Object source, ElapsedEventArgs e)
        {
            // valuta le rules
            var condition = true || false;

            if (condition)
                SetValue();
        }
        private void SetValue()
        {
            //Value = value;
            ValueChanged?.Invoke(this, new SensorEventArgs<Tin, Tout>());
        }
    }
}
