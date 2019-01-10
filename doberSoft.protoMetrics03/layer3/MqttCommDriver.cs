using System;
using System.Collections.Generic;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace doberSoft.protoMetrics03.layer3
{
    class MqttCommDriver: ICommDriver
    {
        string MqttPrefix = "dober/v1/";
        MqttClient client = new MqttClient("broker.hivemq.com");
        string clientId = Guid.NewGuid().ToString();
        SortedList<string, Action<MqttMsgPublishEventArgs>> subscriptions = new SortedList<string, Action<MqttMsgPublishEventArgs>>();

        public event EventHandler<object> MessageReceived;

        public MqttCommDriver()
        {
            // create client instance 
            client.Connect(clientId);
            // publish a message on "/home/temperature" topic with QoS 2 
            client.MqttMsgPublished += client_MqttMsgPublished;
            client.ConnectionClosed += client_ConnectionClosed;
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
        }
        ~MqttCommDriver()
        {
            client.Disconnect();
        }

        public bool Send(string payload, string destination)
        {
            // https://www.hivemq.com/blog/mqtt-client-library-encyclopedia-m2mqtt

            Console.WriteLine("sending " + MqttPrefix + destination + " > " + payload);

            ushort msgId = client.Publish(MqttPrefix + destination, Encoding.UTF8.GetBytes(payload), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            
            return false;
        }
        private void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
            //MqttClient client = (MqttClient)sender;
            //client.Disconnect();
        }
        private void client_ConnectionClosed(object sender, EventArgs e)
        {
            Console.WriteLine("ConnectionClosed: " + e.ToString() );
        }

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // forward the received message to the delegate if there is one, else use the default event
            Action<MqttMsgPublishEventArgs> action;
            if (subscriptions.TryGetValue(e.Topic, out action))
            {
                action.Invoke(e);
                return;
            }
            MessageReceived.Invoke(this, e);
        }
        /// <summary>
        /// The consumer can subscribe to topics and it will receive messages through the default event MessageReceived
        /// Or it can subscribe to a topic and pass a delegate Action to receive the message
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Subscribe(string topic, Action<object> action)
        {
            if (Subscribe(topic))
            {
                // gestire i duplicati
                subscriptions.Add(topic, action);
                return true;
            }
            return false;
        }

        public bool Subscribe(string topic)
        {
            
            // subscribe to the topic with QoS 2
            return client.Subscribe(new string[] { topic }, new byte[] { 2 }) != 0;   // we need arrays as parameters because we can subscribe to different topics with one call
        }

        public void Unsubscribe(string topic)
        {
            client.Unsubscribe(new string[] { topic });
            subscriptions.Remove(topic);
        }
    }

}
