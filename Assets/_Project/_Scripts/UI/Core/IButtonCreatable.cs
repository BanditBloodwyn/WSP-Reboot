using UnityEngine;

namespace Assets._Project._Scripts.UI.Core
{
    public interface IButtonCreatable
    {
        public string ButtonName { get; }
        public Sprite ButtonIcon { get; }
    }
}