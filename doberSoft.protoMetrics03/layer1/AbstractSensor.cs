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
    public abstract class AbstractSensor<Tin, Tout> : ISensor<Tin, Tout>
    {
        private Timer timer;
        protected List<IInput<Tin>> _inputs = new List<IInput<Tin>>();
        private Tin[] _oldInputValues;
        private DateTime _signalTime;


        /// <summary>
        /// Evento generato dal sensore in base alle ScanRules impostate: in polling > allo scadere di ogni intervallo; in push > se la differenza tra il valore attuale e il valore precedente presente sugli ingressi eccede i limiti di isteresi impostati
        /// </summary>
        public event EventHandler<SensorEventArgs> ValueChanged;
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
            Console.WriteLine($"Abstract_{Type}_created({Id})");
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

        public string Name { get; private set; }
        public int Id { get; private set; }
        /// <summary>
        /// Regole utilizzate dal sensore per osservare gli ingressi mappati e generare l'evento ValueChange
        /// </summary>
        public IRules<Tin> Rules { get; private set; }
        /// <summary>
        /// Funzione di scalatura utilizzata per calcolare il valore della grandezza associata al valore dell'input
        /// </summary>
        public IScale<Tin, Tout> ScaleFunction { get; private set; }
        /// <summary>
        /// Valore della grandezza scalata
        /// </summary>
        /// <returns>int, decimal, Position, ...</returns>
        public virtual Tout GetValue()
        {
            var a = _inputs;
            return ScaleFunction.Scale(_inputs.ToArray());
        }
        /// <summary>
        /// Numero di ingressi mappati
        /// </summary>
        protected int InputCount
        {
            get
            {
                return _inputs.Count;
            }
        }

        public string Type {
            get
            {
                return GetType().Name.ToString().Replace("Sensor","").ToLower();
            }
        }

        /// <summary>
        /// Aggiunge un ingresso alla mappatura
        /// </summary>
        /// <param name="input">bool, int, digital</param>
        protected void InputAdd(IInput<Tin> input)
        {
            _inputs.Add(input);
        }
        /// <summary>
        /// Valore attuale presente sugli ingressi
        /// </summary>
        /// <param name="i">Indice dell'ingresso</param>
        /// <returns></returns>
        protected virtual Tin GetCurValue(int i)
        {
            return _inputs[i].GetValue();
        }
        /// <summary>
        /// Valore precedente presente sugli ingressi
        /// </summary>
        /// <param name="i">Indice dell'ingresso</param>
        /// <returns></returns>
        protected virtual Tin GetOldValue(int i)
        {
            return _oldInputValues[i];
        }
        /// <summary>
        /// Arresta il ciclo di osservazione degli ingressi
        /// </summary>
        public void Off()
        {
            timer.Enabled = false;
            timer.AutoReset = false;
            timer.Stop();
            timer.Dispose();
        }
        /// <summary>
        /// Avvia il ciclo di osservazione degli ingressi basandosi sulle Rules impostate
        /// </summary>
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
        /// <summary>
        /// Restituisce il pacchetto (data) contenente il nome, il tipo e l'id del sensore, l'ultimo valore, il timestamp della lettura
        /// </summary>
        /// <returns>SerializeObject(data)</returns>
        public virtual string ToJson()
        {
            var data = new
            {
                type = Type,
                name = Name,
                id = Id,
                value = GetValue(),
                timestamp= _signalTime
            };
            return JsonConvert.SerializeObject(data);
        }
        /// <summary>
        /// Salva i valori presenti sugli input
        /// </summary>
        protected void BackUpInputs()
        {
            for (int i = 0; i < _inputs.Count; i++)
            {
                _oldInputValues[i] = _inputs[i].GetValue();
            }
        }
        /// <summary>
        /// Scansiona gli ingressi con la frequenza impostata e invia i dati in polling
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void tmrPoll_trig(object source, ElapsedEventArgs e)
        {
            SetValue(e);
        }
        /// <summary>
        /// Scansiona gli ingressi con la frequenza impostata e invia i dati solo se superano le isteresi impostate
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected virtual void tmrPush_trig(object source, ElapsedEventArgs e)
        {
            // valuta le rules

            var hHi = Rules.HysteresisHi;
            var hLo = Rules.HysteresisLo;

            for (int i = 0; i < _inputs.Count; i++)
            {
                var result = (dynamic)_inputs[i].GetValue() - (dynamic)_oldInputValues[i];
                if (result > hHi || result < hLo)
                {
                    // appena c'è la condizione avviamo interrompiamo la vaerifica
                    // e inviamo i dati
                    BackUpInputs();
                    SetValue(e);
                    break;
                }
            }
        }
        /// <summary>
        /// Invia i dati al consumer
        /// </summary>
        /// <param name="e"></param>
        protected void SetValue(ElapsedEventArgs e)
        {
            //Console.WriteLine("    SetValue");
            _signalTime = e.SignalTime;
            //ValueChanged?.Invoke(this, new SensorEventArgs<Tin, Tout>());
            ValueChanged?.Invoke(this, new SensorEventArgs());
        }
    }
}
