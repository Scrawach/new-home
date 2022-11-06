using DG.Tweening;
using UnityEngine;

namespace CodeBase.Buildings
{
    public class BuildingProgress : MonoBehaviour
    {
        [SerializeField] 
        private Transform _mesh;

        [SerializeField] 
        private MeshRenderer _renderer;

        [SerializeField] 
        private Vector3 _startPosition;

        [SerializeField] 
        private Vector3 _resultPosition;
        
        [SerializeField]
        private Color[] stateColors;

        private void Awake() => 
            _mesh.localPosition = _startPosition;

        public void UpdateProgress(float progress)
        {
            _mesh.DOKill();
            _mesh.DOLocalMove(Vector3.Lerp(_startPosition, _resultPosition, progress), 1f);
            _mesh.DOShakeScale(.5f, .2f, 10, 90, true);

            if (progress >= 1f)
            {
                _renderer.material.DOColor(stateColors[1], "_EmissionColor", .1f).OnComplete(() => _renderer.material.DOColor(stateColors[0], "_EmissionColor", .5f));
            }
        }
    }
}