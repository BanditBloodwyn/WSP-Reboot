using Assets._Project._Scripts.UI.UIPrefabs.Controls;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Assets._Project._Scripts.CoreFeatures.MapModes.UI
{
    public class MapModesUI : MonoBehaviour
    {
        [Title("")]
        [SerializeField] private GameObject _togglePanel;
        [SerializeField] private GameObject _contentPanel;
        [SerializeField] private ImageButton _button;

        [Title("")]
        [SerializeField] private UnityEvent<MapModeSO> _requestSwitchMapMode;

        private void Awake()
        {
            Assert.IsNotNull(_togglePanel);
            Assert.IsNotNull(_contentPanel);
            Assert.IsNotNull(_button);
        }

        public void TogglePanel()
        {
            _togglePanel.SetActive(!_togglePanel.activeSelf);
        }
        public void OpenPanel()
        {
            _togglePanel.SetActive(true);
        }
        public void ClosePanel()
        {
            _togglePanel.SetActive(false);
        }
    }
}
