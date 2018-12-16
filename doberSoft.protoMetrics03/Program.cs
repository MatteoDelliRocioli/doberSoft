using doberSoft.protoMetrics03.layer0;
using doberSoft.Sensors;
using doberSoft.Sensors.Core;
using doberSoft.Sensors.Core.Rules;
using doberSoft.Sensors.ScaleFunctions;
using System;

namespace doberSoft.protoMetrics03
{
    class Program
    {
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

    }
}
