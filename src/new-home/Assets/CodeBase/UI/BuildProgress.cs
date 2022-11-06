using CodeBase.Buildings;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class BuildProgress : MonoBehaviour
    {
        public Slider Slider;
        public MainCapsule Main;

        private void Start() => 
            Main.StartBuild += OnStartBuild;

        private void OnDestroy() => 
            Main.StartBuild -= OnStartBuild;
        
        private void OnStartBuild()
        {
            var time = Main.BuildRobotTime;
            Slider.gameObject.SetActive(true);
            Slider.DOKill();
            Slider.DOValue(1f, time).OnComplete(() =>
            {
                Slider.value = 0;
                Slider.gameObject.SetActive(false);
            });
        }
    }
}