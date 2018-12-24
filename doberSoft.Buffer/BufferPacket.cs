using doberSoft.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace doberSoft.Buffers 
{
    public class BufferPacket<T>: IBufferPacket<T>//IPacket<T>, IPacketKey
    {
        internal BufferPacket(int priority, DateTime timeStamp, T payload)
        {
            Priority = priority;
            TimeStamp = timeStamp;
            Payload = payload;
        }

        public IPacketKey Key
        {
            get { return this; }
        }

        public string Topic { get; set; }

        public int Priority { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public T Payload { get; private set; }

        public int CompareTo(IPacketKey other)
        {
            /*
             * < zero 	This instance precedes value.
             *   Zero 	This instance has the same position in the sort order as value.
             * > zero 	This instance follows value. -or- value is null. 
             */
            if (Priority != other.Priority)
            {
                return (Priority - other.Priority);
            }
            // same priority: check timestamp
            if (TimeStamp == other.TimeStamp)
            {
                return 0;
            }
            return (TimeStamp < other.TimeStamp) ? -1 : 1;
        }
    }
}