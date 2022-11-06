using CodeBase.Actors;
using CodeBase.GameResources;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Buildings
{
    public class HydroBuilding : Building, ISelectable
    {
        public BuildingHandler Handler;

        public float TimeTickInSeconds;
        public RequiredResources TickRequire;
        public int AddOxygenStorage;

        public CanvasGroup OxygenAdd;

        private float _elapsed;

        private CanvasGroup _previousOxygen;

        public override void OnBuild() => 
            Main.Store(ResourceType.Oxygen, AddOxygenStorage);

        private void Update()
        {
            if (!IsCompleted) 
                return;
            
            _elapsed += Time.deltaTime;
            
            if (_elapsed < TimeTickInSeconds)
                return;
            
            _elapsed = 0;
            
            if (Main.HasResources(TickRequire)) 
                GenerateOxygen();
        }

        private void GenerateOxygen()
        {
            var target = Center();
            target.y = 1.5f;
            _previousOxygen = Instantiate(OxygenAdd, target, Quaternion.identity);
            _previousOxygen.transform.DOMoveY(2f, 2f).SetEase(Ease.OutBack);
            _previousOxygen.DOFade(0, 2f).OnComplete(OnDestroyOxygen);
            Main.UseResources(TickRequire);
        }

        private void OnDestroyOxygen()
        {
            _previousOxygen.DOKill();
            Destroy(_previousOxygen.gameObject);
        }

        public void Select()
        {
            Handler.Select();
        }

        public void Deselect()
        {
            Handler.Deselect();
        }
    }
}