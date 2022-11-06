using System;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraMovement : MonoBehaviour
    {
        public float Speed = 10;
        public float Border = 50;

        public bool Block;

        public Vector2 RangeX;
        public Vector2 RangeY;
        
        private void LateUpdate()
        {
            if (Block)
                return;
            
            var desiredPosition = transform.position;
            var delta = transform.TransformVector(InputMovement());
            delta.y = 0f;
            desiredPosition += delta.normalized * Speed * Time.deltaTime;
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, RangeX.x, RangeX.y);
            desiredPosition.z = Mathf.Clamp(desiredPosition.z, RangeY.x, RangeY.y);
            transform.position = desiredPosition;
        }

        private Vector3 InputMovement()
        {
            var resultDirection = Vector3.zero;
            
            if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - Border)
                resultDirection += Vector3.forward;
            
            if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= Border)
                resultDirection += Vector3.back;
            
            if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - Border)
                resultDirection += Vector3.right;
            
            if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= Border)
                resultDirection += Vector3.left;

            return resultDirection;
        }
    }
}