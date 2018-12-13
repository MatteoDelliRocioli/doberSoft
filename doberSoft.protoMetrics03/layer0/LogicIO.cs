using System;
using System.Timers;

namespace doberSoft.protoMetrics03.layer0
{
    public class LogicIO
    {

        bool[] _bool = new bool[8];
        int[] _analog = new int[8];
        decimal[] _decimal = new decimal[8];
        string[] _string = new string[8];

        internal string GetString(int i)
        {
            return _string[i];
        }


        /// <summary>
        /// Timer interno dell'input scanner: viene attivato chiamando On() e disattivato chiamando Off(). Per modulare 
        /// la velocità di scansione è possibile utilizzare una procedura esterna che richiami Update() 
        /// </summary>
        Timer timer;
        public void Update()
        {
            tmr_trig(null, null);
        }
        

        internal bool GetBool(int i)
        {
            return _bool[i];
        }

        internal int GetInt(int i)
        {
            return _analog[i];
        }

        internal decimal GetDecimal(int i)
        {
            return _decimal[i];
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
            On(100);
        }
        public void On(double interval)
        {
            // iniziaizziamo il loop di osservazione dell'input
            timer = new Timer(interval);
            timer.Elapsed += tmr_trig;
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();
        }

        private void tmr_trig(object source, ElapsedEventArgs e)
        {
            // similazione:
            updateBool(50);
            updateAnalog(50);
            updateDecimal(50);
            // realtà:
            // legge tutti gli ingressi e li scrive sugli array definiti
            // in base alla mappatura del dispositivo
        }


        /********************************************
         ********************************************
         * 
         *          quanto segue simula la
         *          variazione di segnale
         *          degli ingressi fisici
         * 
         ******************************************** 
         ********************************************/
        Random random = new Random();

        public void RndInit()
        {
            for (int i = 0; i < 8; i++)
            {
                int id = random.Next(0, 8);// quale ingresso aggiorniamo
                _bool[i] = random.Next(0, 10) < 5;
                _analog[i] = 280+random.Next(-10, 10);
                _decimal[i] = random.Next(-32767, 32768);
            }

        }
        private void updateBool(int odds)
        {
            int c = random.Next(0, 8);// quanti ingressi aggiorniamo
            for (int i = 0; i < c; i++)
            {
                int id = random.Next(0, 8);// quale ingresso aggiorniamo
                if (random.Next(0, 100) < odds)// con che probabilità lo aggiorniamo
                {
                    if (random.Next(0, 100) < 50)
                        _bool[id] = !_bool[id];
                }
            }
        }

        private void updateAnalog(int odds)
        {
            int c = random.Next(0, 8);// quanti ingressi aggiorniamo
            for (int i = 0; i < c; i++)
            {
                int id = random.Next(0, 8);
                if (random.Next(0, 100) < odds)
                {
                    _analog[id] += random.Next(-5, 6);
                    if (_analog[id] < 0)
                    {
                        _analog[id] = 0;
                    }
                    if (_analog[id] > 1023)
                    {
                        _analog[id] = 1023;
                    }
                }
                //Console.WriteLine($"updateAnalog[{id}] = {_analog[id]}");
            }
        }

        private void updateDecimal(int odds)
            {
                int c = random.Next(0, 8);// quanti ingressi aggiorniamo
                for (int i = 0; i < c; i++)
                {
                    int id = random.Next(0, 8);
                    if (random.Next(0, 100) < odds)
                    {
                        _decimal[id] += random.Next(-5, 6);
                        if (_decimal[id] < -32767)
                        {
                            _decimal[id] = -32767;
                        }
                        if (_decimal[id] > 32768)
                        {
                            _decimal[id] = 32768;
                        }
                    }
            }
        }
    }
}
