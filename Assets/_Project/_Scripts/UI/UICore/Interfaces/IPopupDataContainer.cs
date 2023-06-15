using UnityEngine;

namespace Assets._Project._Scripts.UI.UICore.Interfaces
{
    public interface IPopupDataContainer
    {
        public string Title { get; }

        void ApplyContent(Transform contentPanel);
    }
}