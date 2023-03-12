using TMPro;
using UnityEngine;

namespace Assets._Project._Scripts.UI.Core
{
    public interface IUIDataContainer
    {
        void ApplyHeader(TMP_Text tmpText);
        void ApplyContent(GameObject gameObject);
    }
}