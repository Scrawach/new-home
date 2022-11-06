using CodeBase.Buildings;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI
{
    public class InvalidMessage : MonoBehaviour
    {
        public MainCapsule Main;
        public CanvasGroup Invalid;
        
        private void OnEnable() => 
            Main.NotEnoughMinerals += Message;

        private void OnDisable() => 
            Main.NotEnoughMinerals -= Message;

        private void Message()
        {
            Invalid.DOKill();
            Invalid.gameObject.SetActive(true);
            Invalid.DOFade(1f, 0.2f).OnComplete(() => Invalid.DOFade(0f, 2f).OnComplete(() => Invalid.gameObject.SetActive(false)));
        }
    }
}