using System.Linq;
using UnityEngine;

namespace MajongGame.Configs.Level
{
    [CreateAssetMenu]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField, TextArea(8, 10)] public string FirstLeayerPattern { get; private set; } = "########\n########\n########\n########\n########\n########\n########\n########";
        [field: SerializeField, TextArea(7, 10)] public string SecondLeayerPattern { get; private set; } = "#######\n#######\n#######\n#######\n#######\n#######\n#######";
        [field: SerializeField, TextArea(6, 10)] public string ThirdLeayerPattern { get; private set; } = "######\n######\n######\n######\n######\n######";

        public int TilesCount
        {
            get
            {
                int tilesCountFirstLayer = FirstLeayerPattern.Count((char c) => c == 'P');
                int tilesCountSecondLayer = SecondLeayerPattern.Count((char c) => c == 'P');
                int tilesCountThirdLayer = ThirdLeayerPattern.Count((char c) => c == 'P');

                _tilesCount = tilesCountFirstLayer + tilesCountSecondLayer + tilesCountThirdLayer;
                return _tilesCount;
            }
        }
    
        private int _tilesCount;

    }
}