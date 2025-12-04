using TMPro;
using UnityEngine;

namespace MajongGame.Common.PopupSystem.PopupVariants
{
    public class ConfirmActionPopup : Popup
    {
        [SerializeField] private TMP_Text _confirmTextField;
        private System.Action _confirmAction;

        public void SetConfirm(System.Action action, string actionText = "совершить действие")
        {
            _confirmTextField.text = $"Вы уверены, что хотите {actionText}?";
            _confirmAction = action;
        }
        
        public void Confirm()
        {
            Hide(() => _confirmAction());
        }
    }
}