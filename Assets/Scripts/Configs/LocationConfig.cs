using System.Collections.Generic;
using UnityEngine;

namespace GameTemplate.Configs
{
    [CreateAssetMenu()]
    public class LocationConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Sprite ParticlesImage { get; private set; }
        [field: SerializeField] public List<LevelConfig> Levels { get; private set; }
        [field: SerializeField] public List<Sprite> AvaibleTileSprites { get; private set; }
    }
}