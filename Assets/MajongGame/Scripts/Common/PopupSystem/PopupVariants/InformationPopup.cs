
using TMPro;
using UnityEngine;

namespace MajongGame.Common.PopupSystem.PopupVariants
{
    public class InformationPopup : Popup
    {
        [SerializeField] private TMP_Text _headTextField;
        [SerializeField] private TMP_Text _contentTextField;

        public void SetInfo(string head, string content)
        {
            _headTextField.text = head;
            _contentTextField.text = content;
        }
    }
}