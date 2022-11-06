using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Actors
{
    public class MoveCommand : ICommand
    {
        private readonly Vector3 _targetPosition;
        private readonly ICommand _rotateCommand;
        private readonly CancellationTokenSource _tokenSource = new();

        public MoveCommand(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            _rotateCommand = new RotateToCommand(targetPosition);
        }

        public async UniTask Execute(Actor actor)
        {
            actor.SetDestination(_targetPosition);
            await _rotateCommand.Execute(actor);
            await UniTask.WhenAll(actor.WaitAgentMoved(_tokenSource.Token));
            await _rotateCommand.Execute(actor);
        }

        public void Abort(Actor actor)
        {
            _tokenSource.Cancel();
            _rotateCommand.Abort(actor);
        }
    }
}