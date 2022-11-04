using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Actors
{
    public class ActorHandler : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _selectedSprite;

        [SerializeField] 
        private float _duration = 0.5f;

        private Tween _tween;

        private void Awake()
        {
            _selectedSprite.transform.localScale = Vector3.zero;
        }

        public void Select()
        {
            _tween?.Kill();
            _tween = _selectedSprite.transform.DOScale(1f, _duration).SetEase(Ease.OutBack);
            _selectedSprite.enabled = true;
        }

        public void Deselect()
        {
            _tween?.Kill();
            _tween = _selectedSprite.transform.DOScale(0f, _duration).SetEase(Ease.InBack);
            _tween.OnComplete(() => _selectedSprite.enabled = false);
        }
    }
}