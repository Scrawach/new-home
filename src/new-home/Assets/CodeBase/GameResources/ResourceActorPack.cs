using System;
using UnityEngine;

namespace CodeBase.GameResources
{
    [Serializable]
    public class ResourceActorPack
    {
        [field:SerializeField]
        public ResourceType Type { get; private set; }
        
        [field:SerializeField]
        public int Count { get; private set; }
        
        [field:SerializeField]
        public int Max { get; }

        public event Action Changed;

        public ResourceActorPack(int max) => Max = max;

        public void Add(ResourceType type, int count)
        {
            if (Type == type)
                Count += count;
            else
                Apply(type, count);
            
            Count = Mathf.Clamp(Count, 0, Max);
            Changed?.Invoke();
        }

        private void Apply(ResourceType type, int count)
        {
            Type = type;
            Count = count;
        }

        public bool IsFull() => 
            Count >= Max;

        public int TakeAll()
        {
            var current = Count;
            Count = 0;
            Changed?.Invoke();
            return current;
        }
    }
}