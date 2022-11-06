using System.Threading;
using CodeBase.Buildings;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Actors
{
    public class BuildCommand : ICommand
    {
        private readonly ICommand _command;
        private readonly Building _building;
        private bool _isWorking;
        private readonly CancellationTokenSource _tokenSource = new();

        private readonly RotateToCommand _rotateCommand;

        public BuildCommand(ICommand command, Building building)
        {
            _command = command;
            _building = building;
            _rotateCommand = new RotateToCommand(building.Center());
        }

        public async UniTask Execute(Actor actor)
        {
            await _command.Execute(actor);
            await _rotateCommand.Execute(actor);
            
            _isWorking = true;
            actor.Pieces.Disable();
            actor.LaserGun.Enable();
            while (!_building.IsCompleted && _isWorking)
            {
                _building.Work(_building.WorkForTick);
                actor.LaserGun.UpdateLaser(_building.Center());
                
                if (_building.IsCompleted)
                    break;
                    
                await UniTask.Delay(actor.Stats.BuildPause, cancellationToken: _tokenSource.Token);
            }
            actor.LaserGun.Disable();
        }

        public void Abort(Actor actor)
        {
            _command.Abort(actor);
            _rotateCommand.Abort(actor);
            actor.LaserGun.Disable();
            _isWorking = false;
            _tokenSource.Cancel();
        }
    }
}