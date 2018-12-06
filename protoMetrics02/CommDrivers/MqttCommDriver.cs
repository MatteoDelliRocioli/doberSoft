using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace doberSoft.protoMetrics02.CommDrivers
{
    class MqttCommDriver: ICommDriver
    {
        string MqttPrefix = "dober/v1/";

        public bool Send(string payload, string destination)
        {
            // https://www.hivemq.com/blog/mqtt-client-library-encyclopedia-m2mqtt
            // create client instance 
            MqttClient client = new MqttClient("broker.hivemq.com");
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            Console.WriteLine("sending " + MqttPrefix + destination + " > " + payload);

            // publish a message on "/home/temperature" topic with QoS 2 
            client.MqttMsgPublished += client_MqttMsgPublished;
            client.ConnectionClosed += client_ConnectionClosed;
            ushort msgId = client.Publish(MqttPrefix + destination, Encoding.UTF8.GetBytes(payload), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            
            return false;
        }
        void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
            MqttClient client = (MqttClient)sender;
            client.Disconnect();
        }
        void client_ConnectionClosed(object sender, EventArgs e)
        {
            Console.WriteLine("ConnectionClosed: " + e.ToString() );
        }
    }
}
