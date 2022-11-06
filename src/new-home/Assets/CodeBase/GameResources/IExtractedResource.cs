using CodeBase.Actors;
using UnityEngine;

namespace CodeBase.GameResources
{
    public interface IExtractedResource
    {
        ResourceType Type { get; }
        Vector3 Position { get; }
        bool HasProfit { get; }
        void Work(Actor actor);
    }
}