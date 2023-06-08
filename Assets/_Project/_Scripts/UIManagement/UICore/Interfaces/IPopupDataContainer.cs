using TMPro;
using UnityEngine;

namespace Assets._Project._Scripts.UI.UICore.Interfaces
{
    public interface IPopupDataContainer
    {
        public string ContentIdentifier { get; }
        
        void ApplyHeader(TMP_Text header);
        void ApplyContent(Transform contentPanel);
    }
}