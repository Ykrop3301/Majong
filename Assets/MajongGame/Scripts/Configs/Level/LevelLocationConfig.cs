using System.Collections.Generic;
using UnityEngine;

namespace MajongGame.Configs.Level
{
    [CreateAssetMenu]
    public class LevelLocationConfig : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Background { get; private set; }
        [field: SerializeField] public Sprite LocationImage { get; private set; }
        [field: SerializeField] public Sprite ParticleSprite { get; private set; }
        [field: SerializeField] public List<Sprite> TilePictures { get; private set; }
        [field: SerializeField] public int Reward { get; private set; } = 100;
        [field: SerializeField] public int Cost { get; private set; } = 100;

        [SerializeField] private List<LevelConfig> _levels;

        public int LevelsCount => _levels.Count;

        public LevelConfig GetLevel(int num)
        {
            if (num < 0 || num >= _levels.Count)
                throw new System.Exception($"Invalid level number. {num}");

            return _levels[num];
        }
    }
}
