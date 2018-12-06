using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using doberSoft.protoMetrics02.Sensors;
namespace doberSoft.protoMetrics02.Utils
{
    static class Strings
    {
        public static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }


        public static string ToJson<Tin, Tout>(this ISensor<Tin, Tout> sensor)
        {
            string json = $"{{";
            json = $"{json},\"type\":\"{sensor.Name}\"";
            json = $"{json},\"id\":{sensor.Id}\"";
            // i generici sono castati come object ed espongono solo le proprietà
            // e i metodi di object
            //json = $"{json},\"value\":{sensor.GetValue().ToString("0.00")}\"";
            json = $"{json},\"value\":{sensor.GetValue().ToString()}\"";
            json = $"{json},\"timestamp\":{Strings.GetTimestamp(DateTime.Now) }\"";
            json = $"{json}}}";

            return json;
        }
    }
    static class Actions
    {
        public static void CycleTask(double interval, Action action)
        {
            var timer = new Timer(interval);
            timer.Elapsed += (o, e) => action.Invoke();
            timer.Start();
        }
    }
}
