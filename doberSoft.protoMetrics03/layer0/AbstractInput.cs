using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.layer0
{
    public abstract class AbstractInput<T> : IInput<T>
    {
        protected LogicIO _logicIO;
        protected int _i;


        public AbstractInput(LogicIO logicIO, int i)
        {
            _logicIO = logicIO;
            _i = i;
        }

        public abstract T GetValue();
    }
        
}