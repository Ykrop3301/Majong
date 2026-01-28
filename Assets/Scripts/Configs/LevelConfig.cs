using UnityEngine;

namespace GameTemplate.Configs
{
    [CreateAssetMenu()]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
    }
}