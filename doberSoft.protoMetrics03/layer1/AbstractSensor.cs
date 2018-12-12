using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.ScaleFunctions;
using doberSoft.protoMetrics03.Rules;
using System;
using System.Timers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace doberSoft.protoMetrics03.layer1
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Tin"></typeparam>
    /// <typeparam name="Tout"></typeparam>
    public abstract class AbstractSensor<Tin, Tout> : ISensor<Tin, Tout> //where Tin : new() where Tout : new()
    {
        private Timer timer;
        protected List<IInput<Tin>> _inputs = new List<IInput<Tin>>();
        private Tin[] _oldInputValues;
        private DateTime _signalTime;


        /// <summary>
        /// Evento generato dal sensore in base alle ScanRules impostate: in polling > allo scadere di ogni intervallo; in push > se la differenza il valore attuale e il valore precedente presente sugli ingressi eccede i limiti di isteresi impostati
        /// </summary>
        public event EventHandler<SensorEventArgs<Tin, Tout>> ValueChanged;
        /// <summary>
        /// Restituisce un sensore generico che usa una funzione di scalatura per calcolare il valore gli input
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="scaleFunction"></param>
        /// <param name="rules"></param>
        protected AbstractSensor(
            string name,
            int id,
            IScale<Tin, Tout> scaleFunction,
            IRules<Tin> rules): this(name, id, rules)
        {
            ScaleFunction = scaleFunction;
        }
        /// <summary>
        /// Restituiesce un sensore generico senza funzione di scalatura, gli input non vengono scalati
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="rules"></param>
        protected AbstractSensor(
            string name,
            int id,
            IRules<Tin> rules)
        {
            Id = id;
            Name = name;
            Rules = rules;
        }

        public Type GetInputType
        {
            get
            {
                return typeof(Tin);
            }
        }
        public Type GetOutputType
        {
            get
            {
                return typeof(Tout);
            }
        }

        public string Name { get; private set; }
        public int Id { get; private set; }
        /// <summary>
        /// Regole utilizzate dal sensore per osservare gli ingressi mappati e generare l'evento ValueChange
        /// </summary>
        public IRules<Tin> Rules { get; private set; }
        public IScale<Tin, Tout> ScaleFunction { get; private set; }
        public virtual Tout GetValue()
        {
            var a = _inputs;
            return ScaleFunction.Scale(_inputs.ToArray());
        }
        protected int InputCount
        {
            get
            {
                return _inputs.Count;
            }
        }
        protected void InputAdd(IInput<Tin> input)
        {
            _inputs.Add(input);
        }
        protected virtual Tin GetCurValue(int i)
        {
            return _inputs[i].GetValue();
        }
        protected virtual Tin GetOldValue(int i)
        {
            return _oldInputValues[i];
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
            // inizializziamo il loop di osservazione dell'input
            timer = new Timer(Rules.ScanInterval);
            _oldInputValues = new Tin[_inputs.Count];
            BackUpInputs();

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
            var data = new
            {
                type = Name,
                id = Id,
                value = GetValue(),
                timestamp= _signalTime
            };
            return JsonConvert.SerializeObject(data);
        }
        protected void BackUpInputs()
        {
            for (int i = 0; i < _inputs.Count; i++)
            {
                _oldInputValues[i] = _inputs[i].GetValue();
            }
        }
        private void tmrPoll_trig(object source, ElapsedEventArgs e)
        {
            SetValue(e);
        }
        protected virtual void tmrPush_trig(object source, ElapsedEventArgs e)
        {
            // valuta le rules
            dynamic hHi = Rules.HysteresisHi;
            dynamic hLo = Rules.HysteresisLo;

            for (int i = 0; i < _inputs.Count; i++)
            {
                //Console.WriteLine($"{Name} push({_inputs[i].GetValue()}) - ({_oldInputValues[i]}) = { (dynamic)_inputs[i].GetValue() - (dynamic)_oldInputValues[i]} > {hHi}, < {hLo}");
                var result = (dynamic)_inputs[i].GetValue() - (dynamic)_oldInputValues[i];
                if (result > hHi || result < hLo)
                {
                    //Console.WriteLine("   fire");
                    BackUpInputs();
                    SetValue(e);
                    break;

                }
            }
        }
        protected void SetValue(ElapsedEventArgs e)
        {
            //Console.WriteLine("    SetValue");
            _signalTime = e.SignalTime;
            ValueChanged?.Invoke(this, new SensorEventArgs<Tin, Tout>());
        }
    }
}
