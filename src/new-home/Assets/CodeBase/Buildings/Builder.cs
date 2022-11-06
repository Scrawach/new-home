using System;
using CodeBase.Actors;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace CodeBase.Buildings
{
    public class Builder : MonoBehaviour
    {
        public TaskGiver TaskGiver;
        public MainCapsule Main;
        
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

        [SerializeField]
        private Vector2Int _boardSize = new Vector2Int(10, 10);

        public bool IsDebug;
        public Building[] BakeBuilding;

        private void Awake()
        {
            _camera = Camera.main;
            _ground = new Plane(Vector3.up, Vector3.zero);
            _grid = new BuildingsGrid(_boardSize);

            foreach (var building in BakeBuilding) 
                _grid.Place(building.transform.position.ToVector2Int(), building);
        }

        private void OnDrawGizmos()
        {
            if (!IsDebug)
                return;
            
            for (var x = 0; x < _boardSize.x; x++)
            for (var y = 0; y < _boardSize.y; y++)
            {
                NavMeshHit hit;
                var center = new Vector3(x, 0f, y);
                NavMesh.SamplePosition(center, out hit, 0.4f, 1 << NavMesh.GetAreaFromName("Walkable"));
                if (hit.hit)
                    Gizmos.DrawCube(center, new Vector3(1, 0.1f, 1));
            }
            
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
            if (!Main.HasResources(building.RequiredForBuilding))
            {
                Main.InvokeNotEnough();
                return;
            }
            
            _isProcessing = true;
            _processBuilding = building;
            _previewBuilding = building.GetComponentInChildren<BuildingPreview>();
        }

        private void ResetBuild() => 
            _isProcessing = false;

        private void Build(Vector3 at)
        {
            _isProcessing = false;

            if (!Main.HasResources(_processBuilding.RequiredForBuilding))
            {
                Main.InvokeNotEnough();
                return;
            }
            
            Main.UseResources(_processBuilding.RequiredForBuilding);
            var building = Instantiate(_processBuilding, at, Quaternion.identity);
            building.Construct(Main);
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

        public void Remove(Building building) => 
            _grid.Remove(building.transform.position.ToVector2Int(), building);
    }
}