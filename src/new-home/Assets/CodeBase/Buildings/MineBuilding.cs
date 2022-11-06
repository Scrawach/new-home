using CodeBase.Actors;
using CodeBase.GameResources;
using UnityEngine;

namespace CodeBase.Buildings
{
    public class MineBuilding : Building, ISelectable
    {
        public BuildingHandler Handler;
        public GameObject WorkingFX;

        private bool _isWorking;

        public float TickTimeInSeconds;
        public int MineralsForTick;

        private float _elapsedTime;

        private void Update()
        {
            if (_isWorking)
            {
                _elapsedTime += Time.deltaTime;
                
                if (_elapsedTime >= TickTimeInSeconds)
                {
                    _elapsedTime = 0;
                    Main.Store(ResourceType.Minerals, MineralsForTick);
                }
            }
        }

        private void OnEnable() => 
            BuildDone += OnBuilded;

        private void OnDisable() => 
            BuildDone -= OnBuilded;

        private void OnBuilded()
        {
            _isWorking = true;
        }

        public void Select() => 
            Handler.Select();

        public void Deselect() => 
            Handler.Deselect();
    }
}