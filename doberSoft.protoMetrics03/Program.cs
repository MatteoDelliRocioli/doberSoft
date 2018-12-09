using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.layer1;
using doberSoft.protoMetrics03.Rules;
using doberSoft.protoMetrics03.ScaleFunctions;
using System;

namespace doberSoft.protoMetrics03
{
    class Program
    {
        static void Main(string[] args)
        {
            var logicIO = new LogicIO();

            //ISensor<decimal, Position> sensorPosition = new PositionSensor(
            ISensor<decimal, Position> sensorPosition = new GenericSensor<decimal, Position>(
                "position",
                12,
                new PositionScale(1),
                new ScanRules
                {
                    ScanMode = ScanModeConstants.PollMode,
                    ScanInterval = 500
                },
                logicIO.GetNumericInput(0),
                logicIO.GetNumericInput(1)
                );

            sensorPosition.ValueChanged += Generic_ValueChanged;
            sensorPosition.On();

            ISensor<int, decimal> TemperatureSensor = new GenericSensor<int, decimal>(
                "temperature 1",
                25,
                new LinearScale(0,1024,-200,600),
                new ScanRules
                {
                    ScanMode = ScanModeConstants.PushMode,
                    ScanInterval = 10000
                },
                logicIO.GetAnalogInput(0)
                );

            TemperatureSensor.ValueChanged += Generic_ValueChanged;
            TemperatureSensor.On();

            ISensor<decimal, decimal> busFieldSensor = new GenericSensor<decimal, decimal>(
                "position",
                12,
                new NumericScale(-32767, 32768, 0, 1000),
                new ScanRules
                {
                    ScanMode = ScanModeConstants.PollMode,
                    ScanInterval = 1000
                },
                logicIO.GetNumericInput(2)
                );

            busFieldSensor.ValueChanged += Generic_ValueChanged;
            busFieldSensor.On();

            ISensor<bool, bool> DigitalSensor = new GenericSensor<bool, bool>(
                "digital 1",
                25,
                new DigitalScale(),
                new ScanRules
                {
                    ScanMode = ScanModeConstants.PushMode,
                    ScanInterval = 2000
                },
                logicIO.GetDigitalInput(0)
                );

            DigitalSensor.ValueChanged += Generic_ValueChanged;
            DigitalSensor.On();









            Console.ReadKey();
        }

        private static void Generic_ValueChanged<Tin, Tout>(object sender, SensorEventArgs<Tin, Tout> e)
        {
            var sensor = (ISensor<Tin, Tout>)sender;

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
    }
}
