using System;
using CodeBase.Actors;
using CodeBase.GameResources;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Buildings
{
    public class MainCapsule : Building, ISelectable
    {
        [SerializeField] private BuildingHandler _handler;
        [SerializeField] private ActorFactory _factory;
        
        public int MineralsStored;
        public int OxygenStored;
        public int OxygenMax;

        public int AllEnergy;
        public int UsedEnergy;
        public int AvailableEnergy => AllEnergy - UsedEnergy;
        public int AvailableOxygen => OxygenMax - OxygenStored;

        public event Action<int> MineralsChanged;
        public event Action EnergyChanged;
        public event Action<int> OxygenChanged; 

        public float BuildRobotTime;
        public float ElapsedBuildRobotTime;
        public RequiredResources RobotCost;

        public Transform SpawnPosition;
        
        private bool _isBuilding;

        public event Action StartBuild;
        public event Action NotEnoughMinerals;
        
        public void StoreResource(Actor actor)
        {
            MineralsStored += actor.TakeAllResource().count;
            MineralsChanged?.Invoke(MineralsStored);
        }

        public void TakeEnergy(int count)
        {
            UsedEnergy += count;
            EnergyChanged?.Invoke();
        }

        public void Store(ResourceType type, int count)
        {
            if (type == ResourceType.Minerals)
            {
                MineralsStored += count;
                MineralsChanged?.Invoke(MineralsStored);
            }
            else if (type == ResourceType.Energy)
            {
                AllEnergy += count;
                EnergyChanged?.Invoke();
            }
            else if (type == ResourceType.Oxygen)
            {
                OxygenMax += count;
                OxygenChanged?.Invoke(OxygenStored);
            }
        }

        private void Update()
        {
            if (_isBuilding)
            {
                ElapsedBuildRobotTime += Time.deltaTime;

                if (ElapsedBuildRobotTime >= BuildRobotTime)
                {
                    _isBuilding = false;
                    _factory.CreateRobot(at: SpawnPosition);
                    ElapsedBuildRobotTime = 0;
                }
            }
        }

        public void BuildRobot()
        {
            if (_isBuilding)
                return;

            if (MineralsStored < RobotCost.Minerals || AvailableEnergy < RobotCost.Energy)
            {
                InvokeNotEnough();
                return;
            }

            UseResources(RobotCost);
            _isBuilding = true;
            StartBuild?.Invoke();
        }

        public void InvokeNotEnough() => 
            NotEnoughMinerals?.Invoke();

        public void Select() => 
            _handler.Select();

        public void Deselect() => 
            _handler.Deselect();

        public bool HasResources(RequiredResources resources) =>
            resources.Energy <= AvailableEnergy 
            && resources.Minerals <= MineralsStored 
            && resources.Oxygen <= AvailableOxygen;

        public void UseResources(RequiredResources resources)
        {
            UsedEnergy += resources.Energy;
            MineralsStored -= resources.Minerals;
            OxygenStored += resources.Oxygen;
            
            EnergyChanged?.Invoke();
            MineralsChanged?.Invoke(MineralsStored);
            OxygenChanged?.Invoke(OxygenStored);
        }

        private void OnDestroy() => 
            DOTween.KillAll();
    }
}