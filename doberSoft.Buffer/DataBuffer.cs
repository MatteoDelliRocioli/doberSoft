using System.Collections.Generic;

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
        /// <param name="key">A UID for the operation, used to cancel or confirm
        /// the operation after the fire attempt</param>
        /// <param name="MaxCount">The max number of packets to return at once</param>
        /// <returns>An array of data rapresenting the data packet</returns>
        public IBufferData[] Get(out int key, int MaxCount = -1)
        {

            key = 0;
            /* get the packets in a new list an store it with a key 0 [key]
             * return the list.ToArray()
             */
            var priority = _DataQueues.Keys[0];
            List<IBufferData> packets = _DataQueues.Values[0];
            _DataQueues.RemoveAt(0);
            return packets.ToArray();
        }

        public void Confirm(int key)
        {
            // ritrova la lista (key) e la rimuove da _Queue
            throw new System.NotImplementedException();
        }

        public void Cancel(int key)
        {
            // elimina la lista key
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Appends a data packet to his priority Queue.
        /// There will be several Queues ordered by priority: lower values first
        /// </summary>
        /// <param name="data">The data to be stored into the priority Queue</param>
        /// <param name="priority">The priority order: the lower the value, the higher the priority</param>
        public void Push(IBufferData data, int priority)
        {
            // try to retrieve the data packet List
            // if fail add a new void data packet list
            // append the data to the data packet list

            List<IBufferData> packets;
            if (!_DataQueues.TryGetValue(priority, out packets))
            {
                packets = new List<IBufferData>();
                _DataQueues.Add(priority, packets);
            }
            packets.Add(data);
        }

        public void Print()
        {
            foreach (var item in _DataQueues)
            {
                System.Console.WriteLine($"priority: {item.Key}, Items: {item.Value.Count}");
                foreach (var packet in item.Value)
                {
                    System.Console.WriteLine($"    {packet}");
                }
            }
        }

    }
}