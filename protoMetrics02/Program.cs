using System;
using doberSoft.protoMetrics02.Sensors;
using doberSoft.protoMetrics02.CommDrivers;
using doberSoft.protoMetrics02.Utils;
namespace doberSoft.protoMetrics02
{
    class Program
    {
        static int[] inputs = new int[40];
        static Position position = new Position(-12.34M, 45.67M);

        static void Main(string[] args)
        {
            #region documentation
            /* *******************************************
             * https://www.slideshare.net/MarkLechtermann/mqtt-with-net-core
             * commDriver: ICommDriver
             *                  implementata le diverse modalità richieste per comunicare
             *                  HttpCommDriver: permette di comunicare i dati su piattaforma REST
             *                  MqttCommDriver: permette di comunicare usando protocollo mqtt
             *                  
             * sender: ICommDriver.send(cosa, dove)
             * 
             * cosa: payload, le informazioni da spedire
             *                  singola lettura:
             *                    {type, id, value, time} 
             *                  più letture:
             *                    [{ },
             *                     {type, id, value, time}
             *                     ]  
             *                     
             * dove si compone di:
             *      destination > parte dipendente dal punto di origine delle informazioni
             *                    dalla centralina      /cars/targa/sensors/type  
             *                    verso la centralina   /cars/targa/cmds/type  
             *      +
             *      xxx         > parte dipendente dal driver di connessione
             *      topicPrefix > usiamo mqtt
             *                    IP:port/.../mqtt/v1/
             *      endPoint    > usiamo rest
             *                    http://IP:port/.../v1/
             *                    http://192.168.101.72:8080/dobermetrics/protocol/v1
             *      
             * 
             * la centralina deve ricevere questi comandi
             *      riavvio centralina
             *      apertura porte
             *      avvio motore
             *      spegnimanto motore
             *      
             * Esempi:
             * singola lettura
             *      topicPrefix/cars/AG673WK/sensors/temperature/12
             *                                          {type: temperature, id: 12, value: 21.3, time: 1234523434}
             *
             * letture multiple
             *      topicPrefix//cars/AG673WK/sensors/
             *                                          [{type: temperature, id: 12, value: 21.3, time: 1234523434},
             *                                           {type: temperature, id: 14, value: 21.2, time: 1234523434},
             *                                           {type: speed, id: 35, value: 21.3, time: 1234523436},
             *                                           {type: position, id: 37, value: {45, 45}, time: 1234523436}]
             *                                           
             *      topicPrefix//cars/AG673WK/sensors/temperature/
             *                                          [{type: temperature, id: 12, value: 21.3, time: 1234523434},
             *                                           {type: temperature, id: 14, value: 21.2, time: 1234523434}]
             * 
             *      topicPrefix//cars/AG673WK/cmds/engine/
             *                                          [{type: status, id: 58, value: 1}]
             * 
             * 
             *      
             *      
             * il protocollo dei comandi deve prevedere che ci sia un feedback
             * 
             * */

            #endregion

            ICommDriver CommDriver = new HttpCommDriver();

            #region sensors definition documentation


            /* A sensor is a component used to acquire the device inputs status
             * and notify changes to the consumer.
             * 
             * Each sensor is mapped to a physical input, such as a digital IO,
             * a analog IO, a field or a group of fields in a memory area where
             * an extern device can write throgh the bus
             * 
             * The consumer must instantiate the sensor providing this informations:
             * - the sensor mapped input
             * - its parametrized scaling function
             * - its updater function
             * 
             * When the consumer calls sensor.update(): 
             *  the sensor invokes the updater, which has the reponsability to check
             *  the input status and, if required call sensor.setValue(newValue)
             *  here the sensor fires the event ValueChanged()
             *
             * The GenericSensor accepts:
             *      Tin         Tout        scaleFunction
             * -----------------------------------------------------
             *      int         decimal     LinearScale
             *      int         decimal     ExponentialScale   :TODO
             *      Position    Position    PositionScale
             */
            #endregion

            #region sensors definition 

            var termperature1sensor = new GenericSensor<int, decimal>(
                /* type of sensor   */ "temperature",
                /* numeric id       */ 12,
                /* scaling funcion  */ new LinearScale(0, 1024, -200, 600),
                /* mapped input     */ 35,
                /* delegate updater */ Program.Update_poll<Action>); // polling

            var positionSensor = new GenericSensor<Position, Position>(
                "position",
                44,
                new PositionScale(1),// options: for future use
                58,
                Program.Update_position<Action>);

            var statusSensor = new GenericSensor<int, decimal>(
                "engine",
                58,
                new LinearScale(0, 1, 0, 1),
                0,
                Program.Update_push<Action>);// push notification

            var termperature2sensor = new GenericSensor<int, decimal>(
                "temperature",
                13,
                new LinearScale(0, 1024, -200, 600),
                36,
                Program.Update_poll<Action>);

            var pressureSensor = new GenericSensor<int, decimal>(
                "temperature",
                22,
                new LinearScale(0, 4096, 0, 4),
                9,
                Program.Update_poll<Action>);

            // linking the event processors
            termperature1sensor.ValueChanged += Generic_ValueChanged;
            termperature2sensor.ValueChanged += Generic_ValueChanged;
            pressureSensor.ValueChanged += Generic_ValueChanged;
            positionSensor.ValueChanged += Generic_ValueChanged;
            statusSensor.ValueChanged += Generic_ValueChanged;

            #endregion

            #region initialize and update the input array
            Random random = new Random();

            inputs[35] = 282 + random.Next(-10,10);
            inputs[36] = 282 + random.Next(-10,10);
            inputs[9] = random.Next(0, 1024);
            position.Longitude = 45M +(decimal)random.Next(-500, 500) / 100;
            position.Latitude = -12M + (decimal)random.Next(-500, 500) / 200;

            Actions.CycleTask(500, () =>
            {
                var id = 0;
                if (random.Next(0, 100)>50)// ok, update only 20% of the viewed statuses
                {
                    inputs[id] += random.Next(-1, 2);
                }
                if (inputs[id] < 0)
                {
                    inputs[id] = 0;
                }
                else if (inputs[id] > 1)
                {
                    inputs[id] = 1;
                }

                id = 35;
                inputs[id] += random.Next(-2, 2);
                if (inputs[id] < 0)
                {
                    inputs[id] = 0;
                }
                else if (inputs[id] > 1024)
                {
                    inputs[id] = 1024;
                }
                id = 36;
                inputs[id] += random.Next(-2, 2);
                if (inputs[id] < 0)
                {
                    inputs[id] = 0;
                }
                else if (inputs[id] > 1024)
                {
                    inputs[id] = 1024;
                }
                id = 9;
                inputs[id] += random.Next(-2, 2);
                if (inputs[id] < 0)
                {
                    inputs[id] = 0;
                }
                else if (inputs[id] > 4096)
                {
                    inputs[id] = 4096;
                }

                var pos = position.Longitude;
                pos += (decimal)random.Next(-50, 50) / 100;
                if (pos < -180)
                {
                    pos = -180;
                }
                else if (pos > 180)
                {
                    pos = 180;
                }
                position.Longitude = pos;
                pos = position.Latitude;
                pos += (decimal)random.Next(-50, 50) / 100;
                if (pos < -90)
                {
                    pos = -90;
                }
                else if (pos > 90)
                {
                    pos = 90;
                }
                position.Latitude = pos;
            });
            #endregion

            // update the sensors
            Actions.CycleTask(2000, () =>
            {
                ////Console.WriteLine("\nCycle ");
                //termperature1sensor.Update();
                //termperature2sensor.Update();
                //pressureSensor.Update();
                //positionSensor.Update();
                //statusSensor.Update();

            });

            Console.ReadKey();
        }

        private static void Update_poll<Action>(GenericSensor<int, decimal> sensor, int map, int value)
        {
            value = inputs[map];
            sensor.SetValue(value);
        }

        private static void Update_push<Action>(GenericSensor<int, decimal> sensor, int map, int value)
        {
            if (value == inputs[map])
            {
                return;
            }
            value = inputs[map];
            sensor.SetValue(value);
        }

        private static void Update_position<Action>(GenericSensor<Position, Position> sensor, int map, Position value)
        {
            try
            {
                value.Longitude = position.Longitude;
            } catch
            {
                value = new Position();
                value.Longitude = position.Longitude;
            }

            value.Latitude = position.Latitude;

            sensor.SetValue(value);
        }

        private static void Generic_ValueChanged<Tin, Tout>(object sender, SensorEventArgs<Tin, Tout> e)
        {
            var sensor = (GenericSensor<Tin, Tout>)sender;

            try {
                var json = sensor.ToJson();
                ICommDriver CommDriver = new MqttCommDriver();
                CommDriver.Send(json, "cars/AG673WK/sensors/" + sensor.Name + "/" + sensor.Id);
            }
            catch
            {
                Console.WriteLine("Errore nela generazione del json");
            }
        
        }
    }

}