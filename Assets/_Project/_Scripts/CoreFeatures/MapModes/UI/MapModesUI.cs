using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.UI.UIPrefabs.Controls;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets._Project._Scripts.CoreFeatures.MapModes.UI
{
    public class MapModesUI : MonoBehaviour
    {
        [Title("")]
        [SerializeField] private GameObject _togglePanel;
        [SerializeField] private GameObject _contentPanel;
        [SerializeField] private ImageButton _button;


        #region Unity

        private void OnEnable()
        {
            Events.OnMapModeChosen.AddListener(OnClosePanel);
        }

        private void OnDisable()
        {
            Events.OnMapModeChosen.RemoveListener(OnClosePanel);
        }

        private void Awake()
        {
            Assert.IsNotNull(_togglePanel);
            Assert.IsNotNull(_contentPanel);
            Assert.IsNotNull(_button);
        }

        #endregion


        private object OnClosePanel(Component sender, object data)
        {
            ClosePanel();
            return null;
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
