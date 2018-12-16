using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.Sensors.Core
{
    public class SensorEventArgs : EventArgs
    {
        public SensorEventArgs()
        {
        }
    }
    public class SensorEventArgs<Tin, Tout> : EventArgs
    {
        public SensorEventArgs()
        {
        }
    }
}
