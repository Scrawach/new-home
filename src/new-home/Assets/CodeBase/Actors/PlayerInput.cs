using UnityEngine;

namespace CodeBase.Actors
{
    public class PlayerInput : MonoBehaviour
    {
        private Camera _camera;
        private Plane _ground;

        private void Awake()
        {
            _camera = Camera.main;
            _ground = new Plane(Vector3.up, Vector3.zero);
        }

        public bool IsSelectButtonClicked() => 
            Input.GetMouseButtonDown(0);
        
        public bool IsAcceptButtonClicked() => 
            Input.GetMouseButtonDown(1);

        public bool IsMultiplyCommand() =>
            Input.GetKey(KeyCode.LeftShift);
        
        public bool TryGetFromMousePosition<TObject>(out TObject actor) where TObject : Component
        {
            actor = null;
            
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
                actor = hit.collider.gameObject.GetComponentInParent<TObject>();

            return actor != null;
        }

        public Vector3 MousePositionToWorld()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (_ground.Raycast(ray, out var distance))
                return ray.GetPoint(distance);
            
            return Vector3.zero;
        }
    }
}