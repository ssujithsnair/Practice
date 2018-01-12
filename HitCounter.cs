using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    class LRU
    {
        const int CacheSize = 5;
        int cachemiss = 0;
        private readonly LinkedList<int> q = new LinkedList<int>();
        private readonly Dictionary<int, LinkedListNode<int>> map = new Dictionary<int, LinkedListNode<int>>();
        private LinkedListNode<int> Get(int i)
        {
            LinkedListNode<int> node = null;
            if (map.ContainsKey(i))
            {
                node = map[i];
                q.Remove(node);
                q.AddLast(node);
            }
            else
            {
                cachemiss++;
                if (q.Count == 5)
                    q.RemoveFirst();
                node = q.AddLast(i);
            }
            return node;
        }
    }

    class HitCounter
    {
        const int TotalTime = 3600;
        private readonly int[] Times = new int[TotalTime];
        private readonly int[] Hits = new int[TotalTime];
        public void AddHit(int timestamp)
        {
            int index = timestamp % TotalTime;
            if (Times[index] == timestamp)
                Hits[index]++;
            else
            {
                Times[index] = timestamp;
                Hits[index] = 1;
            }
        }

        public int GetHits(int? timestamp)
        {
            int current = (timestamp != null) ? timestamp.Value : DateTime.Now.Second;
            int hits = 0;
            for (int i = 0; i < TotalTime; i++)
            {
                if ((current - Times[i]) < TotalTime)
                    hits += Hits[i];
            }
            return hits;
        }
    }
}
