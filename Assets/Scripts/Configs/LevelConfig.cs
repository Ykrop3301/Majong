using System.Linq;
using UnityEngine;

namespace GameTemplate.Configs
{
    [CreateAssetMenu()]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField, TextArea(8, 10)] public string FirstLeayerPattern { get; private set; } = "########\n########\n########\n########\n########\n########\n########\n########";
        [field: SerializeField, TextArea(7, 10)] public string SecondLeayerPattern { get; private set; } = "#######\n#######\n#######\n#######\n#######\n#######\n#######";
        [field: SerializeField, TextArea(8, 10)] public string ThirdLeayerPattern { get; private set; } = "########\n########\n########\n########\n########\n########\n########\n########";

        [SerializeField] private int _tilesCount;
        [SerializeField] private bool _isCorrect;

        public int TilesCount
        {
            get
            {
                return _tilesCount;
            }
        }

        private void OnValidate()
        {
            UpdateTilesCount();
        }

        private void UpdateTilesCount()
        {
            int tilesCountFirstLayer = FirstLeayerPattern.Count((char c) => c == 'P');
            int tilesCountSecondLayer = SecondLeayerPattern.Count((char c) => c == 'P');
            int tilesCountThirdLayer = ThirdLeayerPattern.Count((char c) => c == 'P');

            _tilesCount = tilesCountFirstLayer + tilesCountSecondLayer + tilesCountThirdLayer;
            _isCorrect = _tilesCount % 3 == 0 && _tilesCount != 0;
        }
    }
}