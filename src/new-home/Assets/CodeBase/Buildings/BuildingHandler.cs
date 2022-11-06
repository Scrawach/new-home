using System;
using CodeBase.Actors;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Buildings
{
    public class BuildingHandler : MonoBehaviour, ISelectable
    {
        [SerializeField]
        private Transform _selectedSprite;

        [SerializeField] 
        private float _duration = 0.5f;

        private Tween _tween;

        private float _size;

        private void Awake()
        {
            _size = _selectedSprite.localScale.x;
            _selectedSprite.transform.localScale = Vector3.zero;
        }

        public void Select()
        {
            _tween?.Kill();
            _selectedSprite.gameObject.SetActive(true);
            _tween = _selectedSprite.transform.DOScale(_size, _duration).SetEase(Ease.OutBack);
        }

        public void Deselect()
        {
            _tween?.Kill();
            _tween = _selectedSprite.transform.DOScale(0f, _duration).SetEase(Ease.InBack);
            _tween.OnComplete(() => _selectedSprite.gameObject.SetActive(false));
        }
    }
}