using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.UI.UICore.Interfaces;
using Assets._Project._Scripts.UI.UIPrefabs.Controls.TabControl;
using TMPro;
using UnityEngine;

namespace Assets._Project._Scripts.UI.UIPrefabs.Controls
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _header;
        [SerializeField] private Transform _contentPanel;

        public string ContentIdentifier => _header.text;

        public void ApplyData(IPopupDataContainer[] data)
        {
            if (!CreateTabGroup(out TabGroup tabGroup))
                return;

            for (var tabIndex = 0; tabIndex < data.Length; tabIndex++)
            {
                IPopupDataContainer tabContent = data[tabIndex];

                tabGroup.SetTabHeader(tabIndex, tabContent.Title);
                Transform detailsTab = tabGroup.GetTabPageTransform(tabIndex);
                tabContent.ApplyContent(detailsTab);
            }

            tabGroup.SelectTabByIndex(0);
        }

        private bool CreateTabGroup(out TabGroup tabGroup)
        {
            tabGroup = null;

            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.TabControl, out GameObject tabControlPrefab))
                return false;

            GameObject tabControlInstance = Instantiate(tabControlPrefab, _contentPanel.transform);
            tabGroup = tabControlInstance.GetComponentInChildren<TabGroup>();

            return true;
        }

        public void SetTitle(string text)
        {
            _header.text = text;
        }

        // ReSharper disable once UnusedMember.Global
        // Used by the inspector
        public void Close()
        {
            Events.OnCloseTileSelectionPopup.Invoke(this, null);
        }
    }
}