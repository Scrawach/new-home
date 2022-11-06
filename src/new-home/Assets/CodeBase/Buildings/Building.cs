using System;
using System.Text;
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
        
        public RequiredResources RequiredForBuilding;
        
        [field: SerializeField] 
        public float WorkForTick = 10;

        public bool IsComplete;
        public bool IsCompleted => _currentWork >= TotalWorkToComplete || IsComplete;

        public ParticleSystem BuildingFX;
        public AudioSource BuildingSFX;
        private bool _hasPlayed;
        
        public event Action BuildDone;

        private float _currentWork;

        public MainCapsule Main;
        
        public void Construct(MainCapsule main) => 
            Main = main;

        public void Work(float work)
        {
            if (!BuildingFX.isPlaying)
                BuildingFX.Play();

            if (!BuildingSFX.isPlaying && !_hasPlayed)
            {
                _hasPlayed = true;
                BuildingSFX.Play();
            }
            
            if (IsCompleted)
                return;
            
            _currentWork += work;
            _progress.UpdateProgress(_currentWork / TotalWorkToComplete);

            if (IsCompleted)
            {
                OnBuild();
                BuildingFX.Stop();
                BuildDone?.Invoke();
            }
        }

        public virtual void OnBuild() { }

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
