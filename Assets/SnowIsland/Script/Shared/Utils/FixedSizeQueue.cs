using System.Collections.Generic;

namespace TaterGame.SnowIsland.Shared
{
    public class FixedSizeQueue<T> : Queue<T>
    {
        private readonly int _maxSize;
    
        public FixedSizeQueue(int maxSize)
        {
            _maxSize = maxSize;
        }
    
        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            while (Count > _maxSize)
            {
                Dequeue();
            }
        }
    }
}