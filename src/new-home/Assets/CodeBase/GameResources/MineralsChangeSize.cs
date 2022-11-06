using DG.Tweening;
using UnityEngine;

namespace CodeBase.GameResources
{
    public class MineralsChangeSize : MonoBehaviour
    {
        public Transform MineralsTransform;
        public Minerals Minerals;
        public float BoardValue = 0.1f;
        public RemoveFromBuilder RemoveFromBuilder;
        
        private void OnEnable() => 
            Minerals.ProfitChanged += OnProfitChanged;

        private void OnDisable() => 
            Minerals.ProfitChanged -= OnProfitChanged;

        private void Awake() => 
            OnProfitChanged();

        private void OnProfitChanged()
        {
            var desiredScale = (float) Minerals.Profit / Minerals.MaxProfit;
            desiredScale = 0.7f + desiredScale * 0.3f;

            MineralsTransform.localScale = desiredScale * Vector3.one;
            MineralsTransform.DOKill();
            MineralsTransform.DOShakeScale(.5f, .2f, 10, 90, true);

            if (!Minerals.HasProfit)
            {
                RemoveFromBuilder.Remove();
                gameObject.SetActive(false);
            }
        }
    }
}