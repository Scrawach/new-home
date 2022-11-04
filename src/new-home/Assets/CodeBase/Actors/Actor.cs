using System.Threading;
using CodeBase.Effects;
using CodeBase.GameResources;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Actors
{
    public class Actor : MonoBehaviour, ISelectable
    {
        [SerializeField] private NavMeshAgent _agent;

        [SerializeField] private ActorHandler _handler;

        public ResourceActorPieces Pieces;
        public LaserGun LaserGun;
        public ResourceActorPack ResourcePack;
        public ActorStats Stats;

        private ICommand _currentCommand;

        private void Awake() => 
            ResourcePack = new ResourceActorPack(Stats.MaxResourceCount);

        public void SetDestination(Vector3 point) =>
            _agent.destination = point;

        public void Execute(ICommand command)
        {
            Pieces.UpdatePiece(ResourcePack);
            _currentCommand?.Abort(this);
            _currentCommand = command;
            _currentCommand.Execute(this);
        }

        public UniTask WaitAgentMoved(CancellationToken token = default) =>
            UniTask.WaitUntil(() => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance + 1f,
                PlayerLoopTiming.FixedUpdate, cancellationToken: token);

        public void Select() =>
            _handler.Select();

        public void Deselect() =>
            _handler.Deselect();

        private void OnDestroy() =>
            _currentCommand?.Abort(this);
    }
}