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

            ICommDriver CommDriver = new HttpCommDriver();
            var logicIO = new LogicIO();
            logicIO.RndInit();


            ISensor<decimal, Position> posSensor = new PositionSensor(
            //ISensor<decimal, Position> posSensor = new GenericSensor<decimal, Position>(
                "position",
                12,
                new PositionScale(),
                new ScanRules<decimal>
                {
                    ScanMode = ScanModeConstants.PushMode,
                    ScanInterval = 500,
                    HysteresisHi = 5
                },
                logicIO.GetNumericInput(0),
                logicIO.GetNumericInput(1)
                );

            ISensor<int, decimal> tempSensor = new TemperatureSensor(
                "temperature 1",
                25,
                new LinearScale(0, 1024, -200, 600),
                new ScanRules<int>
                {
                    ScanMode = ScanModeConstants.PushMode,
                    ScanInterval = 1000,
                    HysteresisHi = 5,
                    HysteresisLo = -5,
                },
                logicIO.GetAnalogInput(0)
                );


            ISensor<decimal, decimal> busFieldSensor = new GenericSensor<decimal, decimal>(
                "bus field 1",
                12,
                new NumericScale(-32767, 32768, 0, 1000),
                new ScanRules<decimal>
                {
                    ScanMode = ScanModeConstants.PollMode,
                    ScanInterval = 1000
                },
                logicIO.GetNumericInput(2)
                );

            ISensor<bool, bool> bitSensor = new DigitalSensor(
                "digital 1",
                25,
                ScanModeConstants.PushMode,
                2000,
                logicIO.GetDigitalInput(0)
                );


            //posSensor.ValueChanged += PositionSensor_ValueChanged;
            //tempSensor.ValueChanged += TemperatureSensor_ValueChanged;
            //busFieldSensor.ValueChanged += BusFieldSensor_ValueChanged;
            //bitSensor.ValueChanged += DigitalSensor_ValueChanged;
            //posSensor.ValueChanged += Generic_ValueChanged;
            //tempSensor.ValueChanged += Generic_ValueChanged;
            //busFieldSensor.ValueChanged += Generic_ValueChanged;
            //bitSensor.ValueChanged += Generic_ValueChanged;

            //posSensor.On();
            //tempSensor.On();
            //busFieldSensor.On();
            //bitSensor.On();


            // TODO: generare una classe Sensors che permetta di aggiungere e 
            // manipolare i sensori a prescindere dal tipo implementato
            // https://stackoverflow.com/questions/754341/adding-generic-object-to-generic-list-in-c-sharp

            List<ISensor> sensors = new List<ISensor>();
            sensors.Add(bitSensor);
            sensors.Add(tempSensor);
            sensors.Add(busFieldSensor);
            sensors.Add(bitSensor);

            foreach (var item in sensors)
            {
                item.ValueChanged += Generic_ValueChanged;
                item.On();
            }



            logicIO.On();
            //testLogicIO(logicIO);





            Console.ReadKey();
        }


        //private static void Generic_ValueChanged<Tin, Tout>(object sender, SensorEventArgs<Tin, Tout> e)
        //{
        //    var sensor = (ISensor<Tin, Tout>)sender;
        private static void Generic_ValueChanged(object sender, SensorEventArgs e)
        {
            var sensor = (ISensor)sender;

            try
            {
                var json = sensor.ToJson();
                Console.WriteLine($"Generic_ValueChanged |{json}|");
                //ICommDriver CommDriver = new MqttCommDriver();
                //CommDriver.Send(json, "cars/AG673WK/sensors/" + sensor.Name + "/" + sensor.Id);
            }
            catch
            {
                Console.WriteLine("Errore nela generazione del json");
            }

        }

        //private static void PositionSensor_ValueChanged(object sender, SensorEventArgs<decimal, Position> e)
        //{
        //    var sensor = (ISensor<decimal, Position>)sender;

        //    try
        //    {
        //        var json = sensor.ToJson();
        //        Console.WriteLine($"Position_ValueChanged |{json}|");
        //        //ICommDriver CommDriver = new MqttCommDriver();
        //        //CommDriver.Send(json, "cars/AG673WK/sensors/" + sensor.Name + "/" + sensor.Id);
        //    }
        //    catch
        //    {
        //        Console.WriteLine("Errore nela generazione del json");
        //    }
        //}

        //private static void TemperatureSensor_ValueChanged(object sender, SensorEventArgs<int, decimal> e)
        //{
        //    var sensor = (ISensor<int, decimal>)sender;

        //    try
        //    {
        //        var json = sensor.ToJson();
        //        Console.WriteLine($"Temperature_ValueChanged | {json} |");
        //        //ICommDriver CommDriver = new MqttCommDriver();
        //        //CommDriver.Send(json, "cars/AG673WK/sensors/" + sensor.Name + "/" + sensor.Id);
        //    }
        //    catch
        //    {
        //        Console.WriteLine("Errore nela generazione del json");
        //    }
        //}

        //private static void BusFieldSensor_ValueChanged(object sender, SensorEventArgs<decimal, decimal> e)
        //{
        //    var sensor = (ISensor<decimal, decimal>)sender;

        //    try
        //    {
        //        var json = sensor.ToJson();
        //        Console.WriteLine($"Bus_ValueChanged |{json}|");
        //        //ICommDriver CommDriver = new MqttCommDriver();
        //        //CommDriver.Send(json, "cars/AG673WK/sensors/" + sensor.Name + "/" + sensor.Id);
        //    }
        //    catch
        //    {
        //        Console.WriteLine("Errore nela generazione del json");
        //    };
        //}

        //private static void DigitalSensor_ValueChanged(object sender, SensorEventArgs<bool, bool> e)
        //{
        //    var sensor = (ISensor<bool, bool>)sender;

        //    try
        //    {
        //        var json = sensor.ToJson();
        //        Console.WriteLine($"Digital_ValueChanged |{json}|");
        //        //ICommDriver CommDriver = new MqttCommDriver();
        //        //CommDriver.Send(json, "cars/AG673WK/sensors/" + sensor.Name + "/" + sensor.Id);
        //    }
        //    catch
        //    {
        //        Console.WriteLine("Errore nela generazione del json");
        //    }
        //}

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
