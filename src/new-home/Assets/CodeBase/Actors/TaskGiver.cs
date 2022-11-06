using System.Xml.Schema;
using CodeBase.Buildings;
using CodeBase.GameResources;
using DG.Tweening;
using Mono.Cecil;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Actors
{
    public class TaskGiver : MonoBehaviour
    {
        public ActorSelection Selection;
        public PlayerInput Player;
        public GameObject Marker;
        public float MarkerFadeTime;

        public void Update()
        {
            if (!Player.IsAcceptButtonClicked() || Selection.SelectedActors.Count < 1) 
                return;

            if (Player.TryGetFromMousePosition<Building>(out var building) && !building.IsCompleted)
            {
                Build(building);
                return;
            }

            if (Player.TryGetFromMousePosition<IExtractedResource>(out var resource))
            {
                FarmResource(resource);
                return;
            }
            
            if (Player.TryGetFromMousePosition<MainCapsule>(out var main))
            {
                StoreResource();
                return;
            }

            if (Selection.SelectedActors.Count < 2)
                MoveSingleActor();
            else
                MoveGroupActors();
        }

        private void StoreResource()
        {
            foreach (var actor in Selection.SelectedActors) 
                actor.Execute(new HandOverResource());
        }

        private void FarmResource(IExtractedResource resource)
        {
            foreach (var actor in Selection.SelectedActors)
            {
                var targetPoint = resource.Position;
                var direction = (targetPoint - actor.transform.position).normalized;
                targetPoint -= direction * 1.5f;
                Debug.DrawLine(targetPoint, targetPoint + Vector3.up, Color.green, 10f);
                actor.Execute(new WorkCommand(new MoveCommand(targetPoint), resource));
            }
        }

        public void Build(Building building)
        {
            foreach (var actor in Selection.SelectedActors)
            {
                var spherePoint = Random.insideUnitCircle.normalized * building.Size.x / 2;
                var targetPosition = building.Center() + new Vector3(spherePoint.x, 0f, spherePoint.y);
                Debug.DrawLine(targetPosition, targetPosition + Vector3.up, Color.green, 10f);
                actor.Execute(new BuildCommand(new MoveCommand(targetPosition), building));
            }
        }

        private void MoveGroupActors()
        {
            var position = Player.MousePositionToWorld();
            foreach (var actor in Selection.SelectedActors)
            {
                var spherePoint = Random.insideUnitCircle.normalized;
                var movePosition = position + new Vector3(spherePoint.x, 0f, spherePoint.y);
                actor.Execute(new MoveCommand(movePosition));
            }
            Mark(position);
        }

        private void MoveSingleActor()
        {
            var position = Player.MousePositionToWorld();
            
            Selection.SelectedActors[0].Execute(new MoveCommand(position));
            Mark(position);
        }

        private void Mark(Vector3 position)
        {
            Marker.SetActive(true);
            Marker.transform.DOKill();
            Marker.transform.position = position + 0.1f * Vector3.up;
            Marker.transform.localScale = Vector3.one;
            Marker.transform.DOScale(0f, MarkerFadeTime).SetEase(Ease.InBack).OnComplete(() => Marker.SetActive(false));
        }
    }
}