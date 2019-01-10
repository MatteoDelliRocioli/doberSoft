using doberSoft.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace doberSoft.Buffers 
{
    /// <summary>
    /// Base type for buffered data: implements IComparable<IPacketKey> and has a parametric type por Payload
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public class BufferPacket<TPayload>: IBufferPacket<TPayload>
    {
        internal BufferPacket(int priority, DateTime timeStamp, TPayload payload)
        {
            Priority = priority;
            TimeStamp = timeStamp;
            Payload = payload;
        }

        public IPacketKey Key { get => this; }

        public string Topic { get; set; }

        public int Priority { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public TPayload Payload { get; private set; }

        public int StageId { get; set; }

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