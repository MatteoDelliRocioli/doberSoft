using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.layer3
{
    public interface ICommDriver
    {
        //bool Connect();
        bool Send(string payload, string destination);
    }
}
