using UnityEngine;

namespace CodeBase.Effects
{
    public class LaserGun : MonoBehaviour
    {
        public LineRenderer Line;
        public GameObject StartVfx;
        public GameObject EndVfx;

        private RaycastHit[] _hits = new RaycastHit[1];

        public void Enable()
        {
            Line.enabled = true;
            StartVfx.SetActive(true);
            EndVfx.SetActive(true);
        }

        public void Disable()
        {
            Line.enabled = false;
            StartVfx.SetActive(false);
            EndVfx.SetActive(false);
        }

        public void UpdateLaser(Vector3 target)
        {
            var firePoint = transform.position;
            var direction = (target - firePoint).normalized;
            var ray = new Ray(firePoint, direction);
            var targetPosition = Vector3.zero;
            
            Line.SetPosition(0, firePoint);
            var hit = Physics.Raycast(firePoint, direction, out var raycastHit);

            if (hit)
            {
                targetPosition = raycastHit.point;
                targetPosition.y = firePoint.y;
                Line.SetPosition(1, targetPosition);
                EndVfx.transform.position = targetPosition;
                StartVfx.transform.rotation =  Quaternion.LookRotation(direction);
                EndVfx.transform.rotation = Quaternion.LookRotation(-direction);
            }
        }
    }
}