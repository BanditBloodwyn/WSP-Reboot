using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project._Scripts.UI.Controls
{
    public class ImageButton : MonoBehaviour
    { 
        [SerializeField] private Image _image;

        public void SetImage(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}