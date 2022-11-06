using System;
using CodeBase.Actors;
using CodeBase.Buildings;
using UnityEngine;

namespace CodeBase.GameResources
{
    public class Minerals : MonoBehaviour, IExtractedResource, ISelectable
    {
        public BuildingHandler Handler;
        public ResourceType Type => ResourceType.Minerals;
        public Vector3 Position => transform.position + Mesh.localPosition;

        public int Profit = 100;
        public int MaxProfit = 100;

        public bool HasProfit => Profit > 0;

        public event Action ProfitChanged;

        public Transform Mesh;
        
        public void Work(Actor actor)
        {
            if (!HasProfit)
                return;

            var value = actor.Stats.ResourceInTick;
            if (actor.Stats.ResourceInTick > Profit)
                value = Profit;
            
            Profit -= value;
            ProfitChanged?.Invoke();
            actor.ResourcePack.Add(Type, value);
        }

        public void Select() => 
            Handler.Select();

        public void Deselect() => 
            Handler.Deselect();
    }
}