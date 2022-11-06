using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Actors
{
    public class RotateToCommand : ICommand
    {
        private readonly Vector3 _targetPosition;
        private readonly CancellationTokenSource _tokenSource = new();

        public RotateToCommand(Vector3 targetPosition) => 
            _targetPosition = targetPosition;
        
        public async UniTask Execute(Actor actor)
        {
            var result = _targetPosition;
            result.y = actor.transform.position.y;

            if (result == actor.transform.position)
                return;
            
            var rotateToTarget = actor.transform.DOLookAt(result, 0.5f);
            await rotateToTarget.AsyncWaitForCompletion().AsUniTask().AttachExternalCancellation(_tokenSource.Token);
        }

        public void Abort(Actor actor)
        {
            _tokenSource.Cancel();
        }
    }
}