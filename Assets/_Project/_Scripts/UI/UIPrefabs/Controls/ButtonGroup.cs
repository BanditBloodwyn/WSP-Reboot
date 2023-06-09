using TMPro;
using UnityEngine;

namespace Assets._Project._Scripts.UI.UIPrefabs.Controls
{
    public class ButtonGroup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _header;
        [SerializeField] private GameObject _panel;

        public void SetGroupName(string groupName)
        {
            _header.text = groupName;
        }

        public void AddButton(GameObject button)
        {
            button.transform.SetParent(_panel.transform);
        }
    }
}