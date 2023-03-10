using TMPro;
using UnityEngine;

namespace Assets._Project._Scripts.UI.Core
{
    public interface IUIDataContainer
    {
        public string ContentIdentifier { get; }
        
        void ApplyHeader(TMP_Text header);
        void ApplyContent(Transform contentPanel);
    }
}