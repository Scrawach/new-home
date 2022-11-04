using System;
using CodeBase.Actors;
using UnityEngine;

namespace CodeBase.Buildings
{
    public class Builder : MonoBehaviour
    {
        public TaskGiver TaskGiver;
        
        [SerializeField]
        private BuildingPreview _previewBuilding;

        [SerializeField] 
        private Material _validPlaceMaterial;

        [SerializeField] 
        private Material _invalidPlaceMaterial;

        private bool _isProcessing;
        private Building _processBuilding;

        private Camera _camera;
        private Plane _ground;

        private BuildingsGrid _grid;

        private void Awake()
        {
            _camera = Camera.main;
            _ground = new Plane(Vector3.up, Vector3.zero);
            _grid = new BuildingsGrid(new Vector2Int(10, 10));
        }

        private void Update()
        {
            if (!_isProcessing) 
                return;

            var groundPosition = MouseToGroundPoint(_camera, _ground).RoundToInt();
            var boardPoint = groundPosition.ToVector2Int();
            var isValidPlace = _grid.IsValidPlace(boardPoint, _processBuilding.Size);
            RenderBuildingPreview(groundPosition, isValidPlace ? _validPlaceMaterial : _invalidPlaceMaterial);

            if (Input.GetMouseButtonDown(0) && isValidPlace)
            {
                Build(at: groundPosition);
                _grid.Place(boardPoint, _processBuilding);
            }
            
            if (Input.GetMouseButtonDown(1))
                ResetBuild();
        }

        public void Select(Building building)
        {
            _isProcessing = true;
            _processBuilding = building;
            _previewBuilding = building.GetComponentInChildren<BuildingPreview>();
        }

        private void ResetBuild() => 
            _isProcessing = false;

        private void Build(Vector3 at)
        {
            _isProcessing = false;
            var building = Instantiate(_processBuilding, at, Quaternion.identity);
            TaskGiver.Build(building);
        }

        private void RenderBuildingPreview(Vector3 position, Material material)
        {
            Graphics.DrawMesh(_previewBuilding.Renderer.sharedMesh, position + _previewBuilding.Offset, Quaternion.identity, material, 0);
        }

        private static Vector3 MouseToGroundPoint(Camera targetCamera, Plane ground)
        {
            var ray = targetCamera.ScreenPointToRay(Input.mousePosition);
            
            if (ground.Raycast(ray, out var distance))
            {
                var position = ray.GetPoint(distance);
                return position;
            }

            throw new InvalidOperationException("Not found ground!");
        }
    }
}