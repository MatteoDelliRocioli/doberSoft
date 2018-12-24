using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using doberSoft.Util;

namespace doberSoft.Buffers
{
    public class BufferWithPriority<T> : IBuffer<T>
    {
        // https://docs.microsoft.com/it-it/dotnet/api/system.collections.generic.sortedlist-2?view=netframework-4.7.2

        SortedList<IPacketKey, IBufferPacket<T>> _DataQueues = new SortedList<IPacketKey, IBufferPacket<T>>();
        Dictionary<int, IEnumerable<IBufferPacket<T>>> _Staged= new Dictionary<int, IEnumerable<IBufferPacket<T>>>();

        /// <summary>
        /// True if the buffer at least contains 1 data packet
        /// </summary>
        public bool NotEmpty
        {
            get
            {
                return _DataQueues.Count > 0;
            }
        }
        /// <summary>
        /// True if the buffer doesn't contain any data packet
        /// </summary>
        public bool Empty
        {
            get
            {
                return _DataQueues.Count == 0;
            }
        }


        public int Length
        {
            get
            {
                return _DataQueues.Count;
            }
        }


        public void Cancel(int id)
        {
            // elimina la lista key
            _Staged.Remove(id);
        }


        public void Confirm(int id)
        {
            // https://stackoverflow.com/questions/853526/using-linq-to-remove-elements-from-a-listt
            // ritrova la lista (id) e la rimuove da _Queue
            IEnumerable<IBufferPacket<T>> setToRemove;
            if ( _Staged.TryGetValue(id, out setToRemove))
            {
                foreach (var item in setToRemove)
                {
                    if (item != null){
                    _DataQueues.Remove(item.Key); }
                }
                _Staged.Remove(id);
            }
        }


        /// <summary>
        /// Returns the data packet with the higher priority
        /// The retrieved data packet will be no longer available in the Buffer
        /// </summary>
        /// <param name="id">A UID for the operation, used to cancel or confirm
        /// the operation after the fire attempt</param>
        /// <param name="MaxCount">The max number of packets to return at once</param>
        /// <returns>An array of data rapresenting the data packet</returns>
        public IEnumerable<IBufferPacket<T>> Get(out int id, int MaxCount = -1)
        {
            // get an Unique Identifier for the operation
            id = iUID.NewId();

            if (MaxCount == -1 || MaxCount > _DataQueues.Count)
            {
                MaxCount = _DataQueues.Count;
            }

            // gets the first MaxCount elements from the queue
            // stores the elements in a temporary array associated to the id
            //var staged1 = _DataQueues.OrderBy(p => p.Key)
            //                        .Select(p=>p).Take(MaxCount);//.ToArray();
            var staged = (from p in _DataQueues.Values orderby p.Key select p).Take(MaxCount);//.ToArray();
            foreach (var item in staged)
            {
                Console.WriteLine($"staged {item.Key.Priority}.{item.Key.TimeStamp}:  {item.Topic} :  {item.Payload}");
            }
            _Staged.Add(id, staged);

            // returns the retrieved elements
            return staged.ToArray();
        }

        /// <summary>
        /// Appends a data packet to his priority Queue.
        /// There will be several Queues ordered by priority: lower values first
        /// </summary>
        /// <param name="data">The data to be stored into the priority Queue</param>
        /// <param name="priority">The priority order: the lower the value, the higher the priority</param>
        public IBufferPacket<T> Push(int priority, DateTime timeStamp, T payload)
        {
            // append the data to the data packet list
            var packet = new BufferPacket<T>(priority, timeStamp, payload);
            Console.WriteLine($"Push {packet.Topic}: {packet.Payload}");
            if (_DataQueues.Keys.Contains(packet.Key))
            {
                Console.WriteLine($"   ! {packet.Topic}: {packet.Payload}");  
            }
            _DataQueues.Add(packet.Key, packet);
            return packet;
        }



        public void Print()
        {
            foreach (var item in _DataQueues)
            {
                Console.WriteLine($"    {item.Value.Payload}");
            }
        }

    }
}
