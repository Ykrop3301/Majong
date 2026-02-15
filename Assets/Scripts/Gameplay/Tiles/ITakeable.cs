using UniRx;

namespace Gameplay.Tiles
{
    public interface ITakeable
    {
        public IReadOnlyReactiveProperty<bool> Taked {  get; }
    }
}
