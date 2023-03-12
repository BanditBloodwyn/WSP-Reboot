using Assets._Project._Scripts.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project._Scripts.UI.Controls
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _header;
        [SerializeField] private GameObject _contentPanel;

        public void ApplyData(IUIDataContainer data)
        {
            data.ApplyHeader(_header);
            data.ApplyContent(_contentPanel);
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}
