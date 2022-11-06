using System;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraScrollMouse : MonoBehaviour
    {
        private const string MouseScrollWheel = "Mouse ScrollWheel";
        
        public float ScrollSpeed = 5;
        public float Strength = 5;

        public Vector2 Range;

        private float _desiredHeight;

        private void Awake() => 
            _desiredHeight = transform.position.y;

        private void LateUpdate()
        {
            var input = Input.GetAxisRaw(MouseScrollWheel);
            if (input != 0)
                _desiredHeight = MouseWheeling(input, Strength, Range);
            var current = transform.position;
            current.y = SmoothChangeHeight(current.y);
            transform.position = current;
        }

        private float MouseWheeling(float input, float strength, Vector2 range)
        {
            var offset = transform.position.y + input * strength;
            return Mathf.Clamp(offset, range.x, range.y);
        }

        private float SmoothChangeHeight(float previous)
        {
            var path = _desiredHeight - previous;
            previous += ScrollSpeed * path * Time.deltaTime;
            return previous;
        }
    }
}