using Assets._Project._Scripts.UI.UICore.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Project._Scripts.UI.UICore.Controls
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _header;
        [SerializeField] private Transform _contentPanel;
        
        public UnityEvent<Popup> ClosePopup;

        public string ContentIdentifier => _header.text;

        public void ApplyData(IPopupDataContainer data)
        {
            data.ApplyHeader(_header);
            data.ApplyContent(_contentPanel);
        }

        public void Close()
        {
            ClosePopup?.Invoke(this);
        }
    }
}