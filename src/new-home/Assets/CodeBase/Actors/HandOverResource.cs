using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Actors
{
    public class HandOverResource : ICommand
    {
        private MoveCommand _moveToMain;

        public async UniTask Execute(Actor actor)
        {
            actor.Pieces.UpdatePiece(actor.ResourcePack);
            var targetPoint = actor.Main.Center();
            var direction = (targetPoint - actor.transform.position).normalized;
            targetPoint -= direction;
            Debug.DrawLine(targetPoint, targetPoint + Vector3.up, Color.green, 10f);
            _moveToMain = new MoveCommand(targetPoint);
            await _moveToMain.Execute(actor);
            actor.Main.StoreResource(actor);
        }

        public void Abort(Actor actor) => 
            _moveToMain?.Abort(actor);
    }
}