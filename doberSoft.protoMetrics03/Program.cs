using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.layer3;
using doberSoft.Sensors;
using doberSoft.Sensors.Core;
using doberSoft.Sensors.Core.Rules;
using doberSoft.Sensors.ScaleFunctions;
using System;
using doberSoft.Buffers;
using System.Timers;

namespace doberSoft.protoMetrics03
{
    class Program
    {
        static BufferWithPriority<string> buffer = new BufferWithPriority<string>();

        //static Sender Sender = new Sender(new HttpCommDriver(), buffer);
        static Sender Sender = new Sender(new MqttCommDriver(), buffer);
        //static ICommDriver CommDriver = new HttpCommDriver();
        static void Main(string[] args)
        {

            var logicIO = new LogicIO();
            logicIO.RndInit();


            var sl = new SensorsList();

            sl.CreateSensorOfType<TemperatureSensor>("nuovo sTemp", 101)
                            .WithRules(ScanModeConstants.PushMode,1000,-1,1)
                            .WithScaleFunction(new LinearScale(0, 1024, -200, 600))
                            .InputAdd(logicIO.GetAnalogInput(6))
                            .InputAdd(logicIO.GetAnalogInput(7))
                            .Build();

            sl.CreateSensorOfType<StatusSensor>("nuovo sStatus", 102)
                            .WithRules<bool>(ScanModeConstants.PushMode,1000)
                            .InputAdd(logicIO.GetDigitalInput(5))
                            .Build();

            sl.CreateSensorFordata<Position>("nuovo sPos", 103)
                            .WithRules<decimal>(ScanModeConstants.PushMode, 1000, -1, 1)
                            .WithScaleFunction(new PositionScale(0.01M))
                            .InputAdd(logicIO.GetNumericInput(6))
                            .InputAdd(logicIO.GetNumericInput(7))
                            .Build();

            sl.CreateSensorFordata<int>("nuovo sAnalog", 104)
                            .WithRules<int>(ScanModeConstants.PushMode, 1000, -1, 1)
                            .WithScaleFunction(new LinearScale(0, 1024, 0, 4000))
                            .InputAdd(logicIO.GetAnalogInput(5))
                            .Build();

            sl.CreateSensorFordata<decimal>("nuovo sAnalog", 105)
                            .WithRules<decimal>(ScanModeConstants.PushMode, 1000, -1, 1)
                            .WithScaleFunction(new NumericScale(0, 1024, 0, 4000))
                            .InputAdd(logicIO.GetNumericInput(5))
                            .Build();

            sl.CreateSensorFordata<bool>("nuovo sStatus", 106)
                            .WithRules<bool>(ScanModeConstants.PushMode, 1000)
                            .InputAdd(logicIO.GetDigitalInput(5))
                            .Build();

            
            foreach (var item in sl)
            {
                item.ValueChanged += Generic_ValueChanged;
                item.On();
            }


            logicIO.On();
            
            //CycleTask(2000, () => Fire());
            Console.ReadKey();
        }

        /// <summary>
        /// Generic event handler for sensor.ValueChanged.
        /// Here we build the new packet with some informations: priority and source topic
        /// then we put the packet into a buffer.
        /// </summary>
        /// <param name="sender">The sensor that originated the event</param>
        /// <param name="e"></param>
        private static void Generic_ValueChanged(object sender, SensorEventArgs e)
        {
            var sensor = ((ISensor)sender);
            string json = sensor.ToJson();
            string topic = $"cars/AG673WK/sensors/{ sensor.Type }/{ sensor.Id}";
            Console.WriteLine($"GEN  {topic} > |{json}|");
            // push the payload to the buffer: the buffer will create a packet and return it in
            // a IBufferPacket<string> reference
            // so we can also and store the associated topic with it  
            buffer.NewPacket(1, DateTime.Now, sensor.ToJson()).Topic = topic;
            buffer.Push();
        }

        ///// <summary>
        ///// Message sender: this is invoked with a certain logic, decided on the requirements
        ///// and on the limits of the situation, of the sender, etc.
        ///// IF there is a connection
        /////     we get the packets from the buffer 
        /////     AND try to send the packets
        /////     IF we succeed
        /////         we confrim the operation (and the buffer will remove tha packets)
        /////     ELSE
        /////         we cancel the operation
        ///// </summary>
        //public static void Fire()
        //{


        //    // get the buffered messages in a array, identified with a <Id> and pass it to the COMM Driver
        //    // when the messages will be all sent then call buffer.Confirm(id) to cause the buffer
        //    // to discard the sent messages

        //    int id;
        //    var b = buffer.Get(out id);

        //    foreach (var p in b)
        //    {
        //        if (p == null) { Console.WriteLine($"{id}FIRE: null"); }
        //        else {
        //            //Console.WriteLine($"{id}FIRE {p.Topic } >>> |{ p.Payload}|");

        //            if (!CommDriver.Send(p.Payload, p.Topic))
        //            {
        //                buffer.Cancel(id);
        //                return;
        //            }
        //        }
        //    }
        //    buffer.Confirm(id);

        //}
        //public static void CycleTask(double interval, Action action)
        //{
        //    var timer = new Timer(interval);
        //    timer.Elapsed += (o, e) => action.Invoke();
        //    timer.Start();
        //}

    }
}
