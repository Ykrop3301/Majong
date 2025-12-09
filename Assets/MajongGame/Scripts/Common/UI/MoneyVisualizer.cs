using TMPro;
using UnityEngine;

namespace MajongGame.Common.UI
{
    public class MoneyVisualizer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textField;

        private void Update()
        {
            _textField.text = PlayerPrefs.GetInt("Money").ToString();
        }
    }
}