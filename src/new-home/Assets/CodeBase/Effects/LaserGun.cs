using System;
using System.Text;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Effects
{
    public class LaserGun : MonoBehaviour
    {
        public LineRenderer Line;
        public GameObject StartVfx;
        public GameObject EndVfx;
        public AudioSource SFX;

        private bool _hasTarget;
        private Vector3 _targetPosition;

        public void Enable()
        {
            _hasTarget = false;
            Line.enabled = true;
            StartVfx.SetActive(true);
            EndVfx.SetActive(true);
            SFX.Play();
            SFX.DOFade(1f, 0.1f);
        }

        public void Disable()
        {
            _hasTarget = false;
            Line.enabled = false;
            StartVfx.SetActive(false);
            EndVfx.SetActive(false);
            SFX.DOFade(0f, 0.5f).OnComplete(() => SFX.Stop());
        }

        private void Update()
        { 
            Line.SetPosition(0, transform.position);
            if (_hasTarget)
            {
                Line.SetPosition(1, _targetPosition);
                EndVfx.transform.position = _targetPosition;
            }
        }

        public void UpdateLaser(Vector3 target)
        {
            var firePoint = transform.position;
            target.y = firePoint.y;
            var direction = (target - firePoint).normalized;
            var hit = Physics.Raycast(firePoint, direction, out var raycastHit);

            if (hit)
            {
                _hasTarget = true;
                _targetPosition = raycastHit.point;
                EndVfx.transform.position = _targetPosition;
                Line.SetPosition(1, _targetPosition);
                StartVfx.transform.rotation =  Quaternion.LookRotation(direction);
                EndVfx.transform.rotation = Quaternion.LookRotation(-direction);
            }
        }

        private void OnDestroy() => 
            transform.DOKill();
    }
}