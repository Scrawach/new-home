using System.Threading;
using CodeBase.GameResources;
using Cysharp.Threading.Tasks;

namespace CodeBase.Actors
{
    public class WorkCommand : ICommand
    {
        private readonly IExtractedResource _extractedResource;
        private readonly MoveCommand _moveCommand;
        private readonly RotateToCommand _rotateToCommand;
        private readonly CancellationTokenSource _tokenSource = new();
        private bool _isWorking;
        
        public WorkCommand(IExtractedResource extractedResource)
        {
            _extractedResource = extractedResource;
            _moveCommand = new MoveCommand(extractedResource.Position);
            _rotateToCommand = new RotateToCommand(extractedResource.Position);
        }

        public async UniTask Execute(Actor actor)
        {
            await _moveCommand.Execute(actor);
            await _rotateToCommand.Execute(actor);
            
            actor.Pieces.Disable();
            actor.LaserGun.Enable();

            while (!_isWorking)
            {
                _extractedResource.Work(actor);
                
                if (actor.ResourcePack.IsFull())
                    break;
                
                actor.LaserGun.UpdateLaser(_extractedResource.Position);
                await UniTask.Delay(1000, cancellationToken: _tokenSource.Token);
            }

            actor.LaserGun.Disable();
        }

        public void Abort(Actor actor)
        {
            actor.LaserGun.Disable();
            _isWorking = false;
            _moveCommand.Abort(actor);
            _tokenSource.Cancel();
        }
    }
}