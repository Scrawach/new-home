using System;
using CodeBase.Actors;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class WorkProgress : MonoBehaviour
    {
        public Slider Slider;
        public Actor Actor;

        private void Start() => 
            Actor.ResourcePack.Changed += OnResourceChanged;

        private void OnDestroy() => 
            Actor.ResourcePack.Changed -= OnResourceChanged;
        
        private void OnResourceChanged()
        {
            var resource = Actor.ResourcePack;
            Slider.DOComplete();

            if (resource.Count == 0)
            {
                Slider.value = 0;
                Slider.gameObject.SetActive(false);
            }
            else
            {
                Slider.gameObject.SetActive(true);
                var targetValue = (float) resource.Count / resource.Max;
                Slider.DOValue(targetValue, 0.5f);
            }
            
        }
    }
}