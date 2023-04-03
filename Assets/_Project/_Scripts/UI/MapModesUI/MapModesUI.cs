using Assets._Project._Scripts.UI.MapModesUI.MapModes;
using NUnit.Framework;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Project._Scripts.UI.MapModesUI
{
    public class MapModesUI : MonoBehaviour
    {
        [SerializeField] private MapMode[] _mapModes;

        [Title("")]
        [SerializeField] private TMP_Dropdown _dropDownPrefab;
        [SerializeField] private GameObject _iconPrefab;

        [Title("")]
        [SerializeField] private UnityEvent<MapMode> _requestSwitchMapMode;

        #region Unity

        private void Awake()
        {
            Assert.IsNotNull(_iconPrefab);
        }

        private void Start()
        {
            ClearMapModeIcons();
            CreateMapModeIcons();
        }

        private void OnDisable()
        {
            ClearMapModeIcons();
        }

        private void Update()
        {

        }

        #endregion

        private void ClearMapModeIcons()
        {
            Debug.Log("Clear map mode icons");

            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform child = transform.GetChild(i);
                DestroyImmediate(child.gameObject);
            }
        }

        private void CreateMapModeIcons()
        {
            Debug.Log("Create map mode icons");

            if (_mapModes == null || _mapModes.Length == 0)
                return;

            IGrouping<MapModeCategory, MapMode>[] mapModeGroups = _mapModes
                .GroupBy(static mapMode => mapMode.Category)
                .ToArray();

            foreach (IGrouping<MapModeCategory, MapMode> mapModeGroup in mapModeGroups)
            {
                TMP_Dropdown dropDown = Instantiate(_dropDownPrefab, transform);

                List<TMP_Dropdown.OptionData> optionDatas = mapModeGroup
                    .Select(static mapMode => new TMP_Dropdown.OptionData(mapMode.DisplayName, mapMode.UIIcon))
                    .ToList();
                dropDown.AddOptions(optionDatas);
                dropDown.onValueChanged.AddListener(index => OnClick(mapModeGroup.ToArray()[index]));
            }
        }

        private void OnClick(MapMode mapMode)
        {
            _requestSwitchMapMode?.Invoke(mapMode);
        }
    }
}
