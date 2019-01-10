using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.layer3
{
    public interface ICommDriver
    {

        event EventHandler<object> MessageReceived;
        //bool Connect();
        bool Send(string payload, string destination);


        bool Subscribe(string topic, Action<object> action);

        bool Subscribe(string topic);

        void Unsubscribe(string topic);


    }
}
