using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Actors
{
    public class ActorFactory : MonoBehaviour
    {
        public List<Actor> SceneActors;
        
        public IReadOnlyCollection<Actor> Actors => SceneActors;
    }
}