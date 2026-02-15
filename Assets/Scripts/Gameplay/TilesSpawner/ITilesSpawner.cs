using Cysharp.Threading.Tasks;
using GameTemplate.Configs;

namespace Gameplay.TilesSpawner
{
    public interface ITilesSpawner
    {
        public UniTask SpawnByCfg(LevelConfig levelConfig);
    }
}
