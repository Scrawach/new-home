using UnityEngine;

namespace CodeBase.Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField]
        private BuildingProgress _progress;

        [SerializeField] 
        private BuildingPreview _preview;
        
        [field: SerializeField]
        public Vector2Int Size { get; private set; }
        
        [field: SerializeField]
        public float TotalWorkToComplete { get; private set; }

        [field: SerializeField] 
        public float WorkForTick = 10; 

        public bool IsCompleted => _currentWork >= TotalWorkToComplete;

        private float _currentWork;
        
        public void Work(float work)
        {
            _currentWork += work;
            _progress.UpdateProgress(_currentWork / TotalWorkToComplete);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 0.5f, 0f, 0.5f);
            for (var x = 0; x < Size.x; x++)
            for (var z = 0; z < Size.y; z++) 
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, z), new Vector3(1, 0.2f, 1));
        }

        public Vector3 Center() => 
            transform.position + _preview.Offset;
    }
}
