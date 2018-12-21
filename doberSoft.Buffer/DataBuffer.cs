using doberSoft.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace doberSoft.Buffer 
{
    /// <summary>
    /// Stores data Packets grouped by priority
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DataBuffer<T> : IDataBuffer
    {
        // https://docs.microsoft.com/it-it/dotnet/api/system.collections.generic.sortedlist-2?view=netframework-4.7.2

        SortedList<int, List<IBufferData>> _DataQueues = new SortedList<int, List<IBufferData>>();
        SortedList<int, IEnumerable<IBufferData>> _staged = new SortedList<int, IEnumerable<IBufferData>>();


        /// <summary>
        /// True if the buffer at least contains 1 data packet
        /// </summary>
        public bool NotEmpty {
            get
            {
                return _DataQueues.Count>0;
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
                return _DataQueues.Count ;
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
        public IBufferData[] Get(out int id, int MaxCount = -1)
        {
            // get an Unique Identifier for the operation
            id = iUID.NewId();

            /* get the packets in a new list an store it with a key 0 [key]
             * return the list.ToArray()
             */
            var priority = _DataQueues.Keys[0];
            List<IBufferData> packets = _DataQueues.Values[0];

            if (MaxCount == -1 || MaxCount > packets.Count)
            {
                MaxCount = packets.Count;
            }
            var staged = (from p in packets select p).Take(MaxCount);
            _staged.Add(id, staged);

            return staged.ToArray();
        }

        public void Confirm(int id)
        {
            // https://stackoverflow.com/questions/853526/using-linq-to-remove-elements-from-a-listt
            // ritrova la lista (id) e la rimuove da _Queue
            IEnumerable<IBufferData> staged;
            _staged.TryGetValue(id, out staged);
            var setToRemove = new HashSet<IBufferData>(staged);
            _DataQueues.Values[0].RemoveAll(x => setToRemove.Contains(x));
            _staged.Remove(id);
        }

        public void Cancel(int id)
        {
            // elimina la lista key
            _staged.Remove(id);
        }
        /// <summary>
        /// Appends a data packet to his priority Queue.
        /// There will be several Queues ordered by priority: lower values first
        /// </summary>
        /// <param name="data">The data to be stored into the priority Queue</param>
        /// <param name="priority">The priority order: the lower the value, the higher the priority</param>
        public void Push(IBufferData data)
        {
            // try to retrieve the data packet List
            // if fail add a new void data packet list
            // append the data to the data packet list

            List<IBufferData> packets;
            if (!_DataQueues.TryGetValue(data.Priority, out packets))
            {
                packets = new List<IBufferData>();
                _DataQueues.Add(data.Priority, packets);
            }
            packets.Add(data);
        }

        public void Print()
        {
            foreach (var item in _DataQueues)
            {
                Console.WriteLine($"priority: {item.Key}, Items: {item.Value.Count}");
                foreach (var packet in item.Value)
                {
                    Console.WriteLine($"    {packet}");
                }
            }
        }

    }
}