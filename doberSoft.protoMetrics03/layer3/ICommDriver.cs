using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.layer3
{
    interface ICommDriver
    {
        bool Send(string payload, string destination);
    }
}
