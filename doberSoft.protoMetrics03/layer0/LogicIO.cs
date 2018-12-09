using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.layer0
{
    public class LogicIO
    {
        bool[] _bool = new bool[8];
        int[] _resolution = new int[8];
        int[] _analog = new int[8];
        decimal[] _decimal = new decimal[8];

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
    }
}
