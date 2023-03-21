using Assets._Project._Scripts.World.Generation;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Assets._Project._Scripts.Gameplay.Helper
{
    public static class PointerHelper
    {
        public static Vector3 GetCurrentSelectionPosition(out ChunkComponent chunk)
        {
            chunk = null;

            if (EventSystem.current.IsPointerOverGameObject())
                return Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                chunk = hit.transform.GetComponent<ChunkComponent>();
                return hit.point;
            }
            return Vector3.zero;
        }

        public static Vector3 GetCurrentViewPosition()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return Vector3.zero;

            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            return Physics.Raycast(ray, out RaycastHit hit) 
                ? hit.point 
                : Vector3.zero;
        }
    }
}