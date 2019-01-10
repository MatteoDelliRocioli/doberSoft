using System;
using System.Collections.Generic;
using System.Linq;
using doberSoft.Util;

namespace doberSoft.Buffers
{
    /// <summary>
    /// Buffer used to store data with Priority and TimeStamp and retrieve them ordered with a confirmable request
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public class BufferWithPriority<TPayload> : IBuffer<TPayload>
    {
        // https://docs.microsoft.com/it-it/dotnet/api/system.collections.generic.sortedlist-2?view=netframework-4.7.2

        List< IBufferPacket<TPayload>> _buffer = new List< IBufferPacket<TPayload>>();

        // temp built packet
        private BufferPacket<TPayload> packet;

        public event EventHandler NewMessage;

        /// <summary>
        /// True if the buffer at least contains 1 data packet
        /// </summary>
        public bool NotEmpty { get => _buffer.Count > 0; }

        /// <summary>
        /// True if the buffer doesn't contain any data packet
        /// </summary>
        public bool Empty { get => _buffer.Count == 0; }

        public int Length { get => _buffer.Count; }


        public void Cancel(int stageId)
        {
            // Unstages all the packets with Stage = stageId
            var staged = (from p in _buffer where p.StageId == stageId orderby p.Key select p);
            foreach (var item in staged)
            {
                item.StageId = 0;
            }
        }


        public void Confirm(int stageId)
        {
            // https://stackoverflow.com/questions/853526/using-linq-to-remove-elements-from-a-listt
            // removes all the staged packets with Stage = stageId
            //Console.Write($"Confirmed {_buffer.Count} >");
            _buffer.RemoveAll(x => x.StageId == stageId);
            //Console.WriteLine($":> {_buffer.Count} |");

        }


        /// <summary>
        /// Stages N data with the higher priority and older Timestamp into a packet 
        /// </summary>
        /// <param name="stageId">A UID for the operation, used to cancel or confirm
        /// the operation after the fire attempt</param>
        /// <param name="maxCount">The max number of data to return at once</param>
        /// <returns>An array of data rapresenting the data packet</returns>
        public IEnumerable<IBufferPacket<TPayload>> Get(out int stageId, int maxCount = -1)
        {
            // get an Unique Identifier for the operation
            stageId = iUID.NewId();

            if (maxCount == -1 || maxCount > _buffer.Count)
            {
                maxCount = _buffer.Count;
            }

            // gets the first MaxCount elements from the buffer
            // stores the elements in a temporary array associated to the stageId
           // var staged = (from p in _buffer where p.StageId == 0 orderby p.Key select p).Take(maxCount).ToList();
            var staged =_buffer.Where(p => p.StageId == 0).OrderBy(p => p.Key).Take(maxCount).ToList();
            foreach (var item in staged)
            {
                item.StageId = stageId;
                //Console.WriteLine($"staged({item.Stage}) {item.Key.Priority}.{item.Key.TimeStamp}:  {item.Topic} :  {item.Payload}");
            }

            // returns the retrieved elements
            return staged.ToArray();
        }

        /// <summary>
        /// Adds a data packet to the internal Queue and returns it
        /// </summary>
        /// <param name="priority"></param>
        /// <param name="timeStamp"></param>
        /// <param name="payload"></param>
        /// <returns>The new data</returns>
        public IBufferPacket<TPayload> NewPacket(int priority, DateTime timeStamp, TPayload payload)
        {
            packet = new BufferPacket<TPayload>(priority, timeStamp, payload);
            return packet;
        }
        public void Push()
        {
            // append the data to the data packet list
            _buffer.Add(packet);
            Console.WriteLine($"NewPacket({_buffer.Count}) {packet.Topic}: {packet.Payload}");
            NewMessage?.Invoke(this, new EventArgs());
        }


        public void Print()
        {
            foreach (var item in _buffer)
            {
                Console.WriteLine($"    {item.Payload}");
            }
        }

    }
}
