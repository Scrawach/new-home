using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Actors
{
    public class MoveCommand : ICommand
    {
        private readonly Vector3 _targetPosition;
        private readonly CancellationTokenSource _tokenSource = new();

        public MoveCommand(Vector3 targetPosition) => 
            _targetPosition = targetPosition;

        public async UniTask Execute(Actor actor)
        {
            actor.SetDestination(_targetPosition);
            await actor.WaitAgentMoved(_tokenSource.Token);
        }

        public void Abort(Actor actor) => 
            _tokenSource.Cancel();
    }
}