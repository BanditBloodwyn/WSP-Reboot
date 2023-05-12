using Assets._Project._Scripts.UI.Core;
using Assets._Project._Scripts.UI.Managers;
using Assets._Project._Scripts.UI.Managers.Popups;
using TMPro;
using UnityEngine;

namespace Assets._Project._Scripts.UI.Controls
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _header;
        [SerializeField] private Transform _contentPanel;

        public string ContentIdentifier => _header.text;

        public void ApplyData(IPopupDataContainer data)
        {
            data.ApplyHeader(_header);
            data.ApplyContent(_contentPanel);
        }

        public void Close()
        {
            UIManager.Instance.RaiseEvent(PopupEvent.ClosePopup, this);
        }
    }
}