using Cysharp.Threading.Tasks;

namespace CodeBase.Actors
{
    public interface ICommand
    {
        UniTask Execute(Actor actor);
        void Abort(Actor actor);
    }
}