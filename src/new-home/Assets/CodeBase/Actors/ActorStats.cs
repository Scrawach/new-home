using System;

namespace CodeBase.Actors
{
    [Serializable]
    public class ActorStats
    {
        public int BuildPause = 1000;
        public int MaxResourceCount = 100;
        public int ResourceInTick = 10;
    }
}