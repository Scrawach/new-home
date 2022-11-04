using System.Threading;
using CodeBase.Buildings;
using Cysharp.Threading.Tasks;

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
            await new RotateToCommand(_building.Center()).Execute(actor);
            
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
            actor.LaserGun.Disable();
            _isWorking = false;
            _tokenSource.Cancel();
        }
    }
}