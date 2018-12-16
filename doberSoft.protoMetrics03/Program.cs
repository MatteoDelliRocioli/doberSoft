using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.layer1;
using doberSoft.protoMetrics03.layer3;
using doberSoft.protoMetrics03.Rules;
using doberSoft.protoMetrics03.ScaleFunctions;
using System;
using System.Collections.Generic;
using System.Timers;

namespace doberSoft.protoMetrics03
{
    class Program
    {
        static void Main(string[] args)
        {

            var logicIO = new LogicIO();
            logicIO.RndInit();


            //ISensor<decimal, Position> posSensor = new PositionSensor(
            ////ISensor<decimal, Position> posSensor = new GenericSensor<decimal, Position>(
            //    "position",
            //    12,
            //    new PositionScale(),
            //    new ScanRules<decimal>
            //    {
            //        ScanMode = ScanModeConstants.PushMode,
            //        ScanInterval = 500,
            //        HysteresisHi = 0,
            //        HysteresisLo = 0
            //    },
            //    logicIO.GetNumericInput(0),
            //    logicIO.GetNumericInput(1)
            //    );


            //ISensor<int, decimal> tempSensor = new TemperatureSensor(
            //    "temperature 1",
            //    25,
            //    new LinearScale(0, 1024, -200, 600),
            //    new ScanRules<int>
            //    {
            //        ScanMode = ScanModeConstants.PushMode,
            //        ScanInterval = 1000,
            //        HysteresisHi = 5,
            //        HysteresisLo = -5,
            //    },
            //    logicIO.GetAnalogInput(0)
            //    );


            //ISensor<decimal, decimal> busFieldSensor = new GenericSensor<decimal, decimal>(
            //    "bus field 1",
            //    33,
            //    new NumericScale(-32767, 32768, 0, 1000),
            //    new ScanRules<decimal>
            //    {
            //        ScanMode = ScanModeConstants.PollMode,
            //        ScanInterval = 2000
            //    },
            //    logicIO.GetNumericInput(2)
            //    );

            //ISensor<bool, bool> bitSensor = new StatusSensor(
            //    "digital 1",
            //    42,
            //    ScanModeConstants.PushMode,
            //    2000,
            //    logicIO.GetDigitalInput(0)
            //    );
            //ISensor analogSensor = new AnalogSensor("pressure 1", 56, 10, 0, 4000, ScanModeConstants.PushMode, 2000, logicIO.GetAnalogInput(3));

            //List<ISensor> sensors = new List<ISensor>();
            //sensors.Add(posSensor);
            //sensors.Add(tempSensor);
            //sensors.Add(busFieldSensor);
            //sensors.Add(bitSensor);
            //sensors.Add(analogSensor);

            var sl = new SensorsList();

            var s1 = sl.CreateSensorOfType<TemperatureSensor>("nuovo sTemp", 102)
                            .WithRules(ScanModeConstants.PushMode,1000,-1,1)
                            .WithScaleFunction(new LinearScale(0, 1024, -200, 600))
                            .InputAdd(logicIO.GetAnalogInput(6))
                            .InputAdd(logicIO.GetAnalogInput(7))
                            .Build();



            var s2 = sl.CreateSensorOfType<StatusSensor>("nuovo sStatus", 103)
                            .WithRules<bool>(ScanModeConstants.PushMode,1000)
                            .InputAdd(logicIO.GetDigitalInput(5))
                            .Build();

            var s3 = sl.CreateSensorFordata<Position>("nuovo sPos", 104)
                            .WithRules<decimal>(ScanModeConstants.PushMode, 1000, -1, 1)
                            .WithScaleFunction(new PositionScale(0.01M))
                            .InputAdd(logicIO.GetNumericInput(6))
                            .InputAdd(logicIO.GetNumericInput(7))
                            .Build();

            var s4 = sl.CreateSensorFordata<int>("nuovo sAnalog", 105)
                            .WithRules<int>(ScanModeConstants.PushMode, 1000, -1, 1)
                            .WithScaleFunction(new LinearScale(0, 1024, 0, 4000))
                            .InputAdd(logicIO.GetAnalogInput(5))
                            .Build();

            var s5= sl.CreateSensorFordata<decimal>("nuovo sAnalog", 105)
                            .WithRules<decimal>(ScanModeConstants.PushMode, 1000, -1, 1)
                            .WithScaleFunction(new NumericScale(0, 1024, 0, 4000))
                            .InputAdd(logicIO.GetNumericInput(5))
                            .Build();




            foreach (var item in sl)
            {
                item.ValueChanged += Generic_ValueChanged;
                item.On();
            }



            logicIO.On();
            //testLogicIO(logicIO);





            Console.ReadKey();
        }


        private static void Generic_ValueChanged(object sender, SensorEventArgs e)
        {
            try
            {
                var sensor = ((ISensor)sender);
                var json = sensor.ToJson();
                Console.WriteLine($"cars/AG673WK/sensors/{ sensor.Type }/{ sensor.Id} > |{json}|");
                //ICommDriver CommDriver = new HttpCommDriver();
                //ICommDriver CommDriver = new MqttCommDriver();
                //CommDriver.Send(json, "cars/AG673WK/sensors/" + sensor.Name + "/" + sensor.Id);
            }
            catch
            {
                Console.WriteLine("Errore nela generazione del json");
            }

        }

        private static void testLogicIO(LogicIO logicIO)
        {
            CycleTask(1000, () =>
            {
                Console.WriteLine($"Num0: {logicIO.GetNumericInput(0).GetValue()}");
                Console.WriteLine($"Num1: {logicIO.GetNumericInput(1).GetValue()}");
                Console.WriteLine($"Num2: {logicIO.GetNumericInput(2).GetValue()}");
                Console.WriteLine($"Int0: {logicIO.GetAnalogInput(0).GetValue()}");
                Console.WriteLine($"Int1: {logicIO.GetAnalogInput(1).GetValue()}");
                Console.WriteLine($"Int2: {logicIO.GetAnalogInput(2).GetValue()}");
                Console.WriteLine($"Dig0: {logicIO.GetDigitalInput(0).GetValue()}");
                Console.WriteLine($"Dig1: {logicIO.GetDigitalInput(1).GetValue()}");
                Console.WriteLine($"Dig2: {logicIO.GetDigitalInput(2).GetValue()}");
                Console.WriteLine();
            });
        }

        public static void CycleTask(double interval, Action action)
        {
            var timer = new Timer(interval);
            timer.Elapsed += (o, e) => action.Invoke();
            timer.Start();
        }
    }
}
