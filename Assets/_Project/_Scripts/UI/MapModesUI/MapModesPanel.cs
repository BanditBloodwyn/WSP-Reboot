using Assets._Project._Scripts.Core.Enum.PrefabLists;
using Assets._Project._Scripts.Core.Types;
using Assets._Project._Scripts.UI.Controls;
using Assets._Project._Scripts.UI.MapModesUI.MapModes;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets._Project._Scripts.UI.MapModesUI
{
    public class MapModesPanel : MonoBehaviour
    {
        [SerializeField] private ImageButton _button;
        [SerializeField] private GameObject _contentPanel;
        [SerializeField] private UnityEvent<MapMode> _requestSwitchMapMode;

        #region Unity

        private void Awake()
        {
            Assert.IsNotNull(_button);
        }

        private void Start()
        {
            ClearMapModeButtons();
            CreateMapModeButtons();
        }

        private void OnDisable()
        {
            ClearMapModeButtons();
        }

        #endregion

        private void ClearMapModeButtons()
        {
            Debug.Log("<color=#73BD73>Map mode system</color> - Clear map mode icons");

            for (int i = _contentPanel.transform.childCount - 1; i >= 0; i--)
            {
                Transform child = _contentPanel.transform.GetChild(i);
                DestroyImmediate(child.gameObject);
            }
        }

        private void CreateMapModeButtons()
        {
            Debug.Log("<color=#73BD73>Map mode system</color> - Create map mode icons");

            MapMode[] mapModes = FindMapModes();

            if (mapModes == null || mapModes.Length == 0)
                return;

            IGrouping<MapModeCategory, MapMode>[] mapModeGroups = mapModes
                .GroupBy(static mapMode => mapMode.Category)
                .ToArray();

            CreateButtonGroups(mapModeGroups);
        }

        private static MapMode[] FindMapModes()
        {
            return ScriptableObjectUtils.FindAssetsOfType<MapMode>();
        }

        private void CreateButtonGroups(IGrouping<MapModeCategory, MapMode>[] mapModeGroups)
        {
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.IconGroup, out GameObject buttonGroupPrefab) ||
                !UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.Icon, out GameObject buttonPrefab))
            {
                Debug.LogError("Prefabs not found!");
                return;
            }

            foreach (IGrouping<MapModeCategory, MapMode> mapModeGroup in mapModeGroups)
            {
                GameObject buttonGroupObject = Instantiate(buttonGroupPrefab, _contentPanel.transform);
                ButtonGroup buttonGroup = buttonGroupObject.GetComponent<ButtonGroup>();
                buttonGroup.SetGroupName(mapModeGroup.Key.ToString());

                foreach (MapMode mapMode in mapModeGroup)
                {
                    GameObject instance = Instantiate(buttonPrefab);
                    buttonGroup.AddButton(instance);

                    ImageButton imageButton = instance.GetComponent<ImageButton>();
                    imageButton.SetImage(mapMode.ButtonIcon);

                    Button button = instance.GetComponentInChildren<Button>();
                    button.onClick.AddListener(delegate { OnClick(mapMode); });
                }
            }

        }

        private void OnClick(MapMode mapMode)
        {
            _requestSwitchMapMode?.Invoke(mapMode);
            _button.SetImage(mapMode.ButtonIcon);
        }
    }
}