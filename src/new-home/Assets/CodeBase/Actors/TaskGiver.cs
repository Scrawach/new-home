using CodeBase.Buildings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Actors
{
    public class TaskGiver : MonoBehaviour
    {
        public ActorSelection Selection;
        public PlayerInput Player;

        public void Update()
        {
            if (!Player.IsAcceptButtonClicked() || Selection.SelectedActors.Count < 1) 
                return;

            if (Player.TryGetFromMousePosition<Building>(out var building) && !building.IsCompleted)
            {
                Build(building);
                return;
            }

            if (Selection.SelectedActors.Count < 2)
                MoveSingleActor();
            else
                MoveGroupActors();
        }

        public void Build(Building building)
        {
            foreach (var actor in Selection.SelectedActors)
            {
                var spherePoint = Random.insideUnitCircle.normalized * building.Size.x;
                var targetPosition = building.transform.position + new Vector3(spherePoint.x, 0f, spherePoint.y);
                actor.Execute(new BuildCommand(new MoveCommand(targetPosition), building));
            }
        }

        private void MoveGroupActors()
        {
            foreach (var actor in Selection.SelectedActors)
            {
                var spherePoint = Random.insideUnitCircle.normalized;
                var movePosition = Player.MousePositionToWorld() + new Vector3(spherePoint.x, 0f, spherePoint.y);
                actor.Execute(new MoveCommand(movePosition));
            }
        }

        private void MoveSingleActor()
        {
            Selection.SelectedActors[0].Execute(new MoveCommand(Player.MousePositionToWorld()));
        }
    }
}