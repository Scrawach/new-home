using UnityEngine;

namespace CodeBase.GameResources
{
    public class ResourceActorPack
    {
        public ResourceType Type { get; private set; }
        public int Count { get; private set; }
        public int Max { get; }

        public ResourceActorPack(int max) => Max = max;

        public void Add(ResourceType type, int count)
        {
            if (Type == type)
                Count += count;
            else
                Apply(type, count);
            
            Count = Mathf.Clamp(Count, 0, Max);
        }

        private void Apply(ResourceType type, int count)
        {
            Type = type;
            Count = count;
        }

        public bool IsFull() => 
            Count >= Max;
    }
}