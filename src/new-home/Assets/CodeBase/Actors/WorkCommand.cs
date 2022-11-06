using System.Threading;
using System.Threading.Tasks;
using CodeBase.GameResources;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Actors
{
    public class WorkCommand : ICommand
    {
        private readonly IExtractedResource _extractedResource;
        private readonly MoveCommand _moveCommand;
        private readonly RotateToCommand _rotateToCommand;
        private readonly CancellationTokenSource _tokenSource = new();
        private bool _isWorking;
        
        private readonly HandOverResource _handOverResource = new();

        public WorkCommand(MoveCommand moveCommand, IExtractedResource extractedResource)
        {
            _extractedResource = extractedResource;
            _moveCommand = moveCommand;
            _rotateToCommand = new RotateToCommand(extractedResource.Position);
        }

        public async UniTask Execute(Actor actor)
        {
            await MoveToResource(actor);
            
            actor.Pieces.Disable();
            actor.LaserGun.Enable();

            while (!_isWorking)
            {
                _extractedResource.Work(actor);

                if (actor.ResourcePack.IsFull() || !_extractedResource.HasProfit)
                {
                    actor.LaserGun.Disable();
                    await _handOverResource.Execute(actor);
                    await MoveToResource(actor);

                    if (!_extractedResource.HasProfit)
                        break;
                    
                    actor.LaserGun.Enable();
                }
                
                actor.LaserGun.UpdateLaser(_extractedResource.Position);
                await UniTask.Delay(1000, cancellationToken: _tokenSource.Token);
            }
            
            actor.LaserGun.Disable();
        }

        private async Task MoveToResource(Actor actor)
        {
            await _moveCommand.Execute(actor);
            await _rotateToCommand.Execute(actor);
        }
        
        public void Abort(Actor actor)
        {
            actor.LaserGun.Disable();
            _isWorking = false;
            _rotateToCommand.Abort(actor);
            _moveCommand.Abort(actor);
            _handOverResource?.Abort(actor);
            _tokenSource.Cancel();
        }
    }
}