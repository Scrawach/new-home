using UnityEngine;

namespace CodeBase.Buildings
{
    public class BuildingProgress : MonoBehaviour
    {
        [SerializeField] 
        private Transform _mesh;

        [SerializeField] 
        private Vector3 _startPosition;

        [SerializeField] 
        private Vector3 _resultPosition;

        private void Awake() => 
            _mesh.localPosition = _startPosition;

        public void UpdateProgress(float progress) => 
            _mesh.localPosition = Vector3.Lerp(_startPosition, _resultPosition, progress);
    }
}