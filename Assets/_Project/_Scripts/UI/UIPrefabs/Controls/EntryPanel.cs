using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace Assets._Project._Scripts.UI.UIPrefabs.Controls
{
    public class EntryPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _value;

        private void Awake()
        {
            Assert.IsNotNull(_title);
            Assert.IsNotNull(_value);
        }

        public void Set(string title, string value)
        {
            _title.text = title;
            _value.text = value;
        }
    }
}
