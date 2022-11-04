using System.Threading;
using CodeBase.Buildings;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Actors
{
    public class BuildCommand : ICommand
    {
        private readonly ICommand _command;
        private readonly Building _building;
        private bool _isWorking;
        private readonly CancellationTokenSource _tokenSource = new();

        public BuildCommand(ICommand command, Building building)
        {
            _command = command;
            _building = building;
        }

        public async UniTask Execute(Actor actor)
        {
            await _command.Execute(actor);
            actor.SetDestination(actor.transform.position);
            var rotateToTarget = actor.transform.DOLookAt(_building.transform.position, 0.5f);
            await rotateToTarget.AsyncWaitForCompletion().AsUniTask().AttachExternalCancellation(_tokenSource.Token);
            
            _isWorking = true;
            actor.LaserGun.Enable();
            while (!_building.IsCompleted && _isWorking)
            {
                _building.Work(10);
                actor.LaserGun.UpdateLaser(_building.transform.position);
                await UniTask.Delay(1000, cancellationToken: _tokenSource.Token);
            }
            actor.LaserGun.Disable();
        }

        public void Abort(Actor actor)
        {
            _command.Abort(actor);
            actor.LaserGun.Disable();
            _isWorking = false;
            _tokenSource.Cancel();
        }
    }
}