using Assets._Project._Scripts.Core.Types;
using Assets._Project._Scripts.UI.Controls;
using Assets._Project._Scripts.UI.Core;
using UnityEngine;

namespace Assets._Project._Scripts.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject _movablePopupPreset;
        [SerializeField] private Transform _movablePopupParent;

        public void OpenMovablePopup(IUIDataContainer data)
        {
            GameObject popup = Instantiate(_movablePopupPreset, _movablePopupParent);
            popup.GetComponent<Popup>().ApplyData(data);
        }
    }
}
