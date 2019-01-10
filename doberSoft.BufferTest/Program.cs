using System;
using System.Threading;
using doberSoft.Buffers;

namespace doberSoft.BufferTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //BufferPacket a = new BufferPacket(1, DateTime.Now);
            //BufferPacket b = new BufferPacket(1, DateTime.Now);
            //BufferPacket c = new BufferPacket(1, a.Key.TimeStamp);
            var t = DateTime.Now;
            int id;
            BufferWithPriority<string> buffer = new BufferWithPriority<string>();
            buffer.NewPacket(1, t, "ciao");
            buffer.Push();
            buffer.NewPacket(1, t, "miao");
            buffer.Push();
            var b = buffer.Get(out id);
            foreach (var item in b)
            {
                Console.WriteLine($"p: {item.Key.Priority},t : {item.Key.TimeStamp}, m: {item.Topic}, l: {item.Payload}");
            }
            //Console.WriteLine($"a: b {a.CompareTo(b)}");

            //Console.WriteLine($"a: c {a.CompareTo(c)}");

            //Console.WriteLine($"b: a {b.CompareTo(a)}");
            Console.ReadKey();
        }
    }
}
