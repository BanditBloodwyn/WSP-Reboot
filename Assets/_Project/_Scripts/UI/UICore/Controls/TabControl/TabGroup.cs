using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project._Scripts.UI.UICore.Controls.TabControl
{
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> _tabButtons;
        public List<GameObject> _tabPages;
        public TabButton _selectedTabButton;

        public Color _tabIdleColor;
        public Color _tabHoverColor;
        public Color _tabSelectedColor;

        public void Subscribe(TabButton button)
        {
            _tabButtons ??= new List<TabButton>();
            _tabButtons.Add(button);
        }

        public void OnTabEnter(TabButton button)
        {
            ResetTabs();

            if(_selectedTabButton == null || button != _selectedTabButton) 
                button._image.color = _tabHoverColor;
        }

        public void OnTabExit(TabButton button)
        {
            ResetTabs();
        }

        public void OnTabSelected(TabButton button)
        {
            _selectedTabButton = button;
            ResetTabs();
            button._image.color = _tabSelectedColor;

            int index = button.transform.GetSiblingIndex();

            for (int i = 0; i < _tabPages.Count; i++)
                _tabPages[i].SetActive(i == index);
        }

        public void ResetTabs()
        {
            foreach (TabButton tabButton in _tabButtons)
            {
                if(_selectedTabButton != null && tabButton == _selectedTabButton)
                    continue;

                tabButton._image.color = _tabIdleColor;
            }
        }

        public Transform GetTabPageTransform(int index)
        {
            return _tabPages[index].transform;
        }

        public void SetTabHeader(int index, string text)
        {
            _tabButtons[index].SetTabHeader(text);
        }

        public void SelectTabByIndex(int index)
        {
            if (_tabButtons.Count < index)
                return;

            OnTabSelected(_tabButtons[index]);
        }
    }
}
