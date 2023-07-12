using Assets._Project._Scripts.Core.Data.Types;
using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.UI.UIPrefabs;
using Assets._Project._Scripts.UI.UIPrefabs.Controls;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Assets._Project._Scripts.CoreFeatures.MapModes.UI
{
    public class MapModesPanel : MonoBehaviour
    {
        [SerializeField] private ImageButton _button;
        [SerializeField] private GameObject _contentPanel;

        #region Unity

        private void Awake()
        {
            Assert.IsNotNull(_button);
            Assert.IsNotNull(_contentPanel);
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

            MapModeSO[] mapModes = FindMapModes();

            if (mapModes == null || mapModes.Length == 0)
                return;

            IGrouping<MapModeCategory, MapModeSO>[] mapModeGroups = mapModes
                .GroupBy(static mapMode => mapMode.Category)
                .ToArray();

            CreateButtonGroups(mapModeGroups);
        }

        private static MapModeSO[] FindMapModes()
        {
            return ScriptableObjectUtils.FindAssetsOfType<MapModeSO>();
        }

        private void CreateButtonGroups(IGrouping<MapModeCategory, MapModeSO>[] mapModeGroups)
        {
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.IconButtonGroup, out GameObject buttonGroupPrefab) ||
                !UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.IconButton, out GameObject buttonPrefab))
            {
                Debug.LogError("Prefabs not found!");
                return;
            }

            foreach (IGrouping<MapModeCategory, MapModeSO> mapModeGroup in mapModeGroups)
            {
                GameObject buttonGroupObject = Instantiate(buttonGroupPrefab, _contentPanel.transform);
                ButtonGroup buttonGroup = buttonGroupObject.GetComponent<ButtonGroup>();
                buttonGroup.SetGroupName(mapModeGroup.Key.ToString());

                foreach (MapModeSO mapMode in mapModeGroup)
                {
                    GameObject instance = Instantiate(buttonPrefab);
                    buttonGroup.AddButton(instance);

                    ImageButton imageButton = instance.GetComponent<ImageButton>();
                    imageButton.SetImage(mapMode.UIIcon);

                    Button button = instance.GetComponentInChildren<Button>();
                    button.onClick.AddListener(delegate { OnClick(mapMode); });
                }
            }

        }

        private void OnClick(MapModeSO mapMode)
        {
            Events.OnRequestSwitchMapMode.Invoke(this, mapMode);
            _button.SetImage(mapMode.UIIcon);
        }
    }
}