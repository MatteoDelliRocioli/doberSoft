using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics02.CommDrivers
{
    interface ICommDriver
    {
        bool Send(string payload, string destination);
    }
}
