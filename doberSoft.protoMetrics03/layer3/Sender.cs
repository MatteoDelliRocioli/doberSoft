using doberSoft.Buffers;
using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.layer3
{
    public class Sender
    {

        private ICommDriver CommDriver;
        //private IBuffer<string> Buffer;

        public Sender(ICommDriver commDriver, IBuffer<string> buffer)
        {
            CommDriver = commDriver;
            //Buffer = buffer;
            buffer.NewMessage += (e, o) => {
                // object sender, EventArgs e

                int id;
                var messages = buffer.Get(out id);

                foreach (var message in messages)
                {
                    if (message == null) { Console.WriteLine($"{id}FIRE: null"); }
                    else
                    {
                        //Console.WriteLine($"{id}FIRE {message.Topic } >>> |{ message.Payload}|");

                        if (!CommDriver.Send(message.Payload, message.Topic))
                        {
                            buffer.Cancel(id);
                            return;
                        }
                    }
                }
                buffer.Confirm(id);
            };
        }
    }
}
