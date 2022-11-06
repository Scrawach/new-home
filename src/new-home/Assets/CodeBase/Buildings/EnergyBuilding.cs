using System;
using CodeBase.Actors;
using CodeBase.GameResources;
using UnityEngine;

namespace CodeBase.Buildings
{
    public class EnergyBuilding : Building, ISelectable
    {
        public BuildingHandler Handler;
        public GameObject WorkingFX;

        public int EnergyCount;
        private bool _isWorking;

        private void OnEnable() => 
            BuildDone += OnBuilded;

        private void OnDisable() => 
            BuildDone -= OnBuilded;

        private void OnBuilded()
        {
            _isWorking = true;
            WorkingFX.gameObject.SetActive(true);
            Main.Store(ResourceType.Energy, EnergyCount);
        }

        public void Select() => 
            Handler.Select();

        public void Deselect() => 
            Handler.Deselect();
    }
}