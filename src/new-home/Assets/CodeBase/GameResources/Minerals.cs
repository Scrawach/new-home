using CodeBase.Actors;
using UnityEngine;

namespace CodeBase.GameResources
{
    public class Minerals : MonoBehaviour, IExtractedResource
    {
        public ResourceType Type => ResourceType.Minerals;
        public Vector3 Position => transform.position;

        public void Work(Actor actor) => 
            actor.ResourcePack.Add(Type, actor.Stats.ResourceInTick);
    }
}