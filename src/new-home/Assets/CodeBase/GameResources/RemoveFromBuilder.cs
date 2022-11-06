using CodeBase.Buildings;
using UnityEngine;

namespace CodeBase.GameResources
{
    public class RemoveFromBuilder : MonoBehaviour
    {
        public Building Building;
        public Builder Builder;

        public void Remove() => 
            Builder.Remove(Building);
    }
}