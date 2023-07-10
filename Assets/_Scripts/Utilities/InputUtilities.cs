using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Utilities
{
    internal class InputUtilities
    {
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 v = GetMouseWorldPositionWithZ(Mouse.current.position.ReadValue(), Camera.main);
            v.z = 0;
            return v;
        }
        public static Vector3 GetMouseWorldPositionWithZ()
        {
            return GetMouseWorldPositionWithZ(Mouse.current.position.ReadValue(), Camera.main);
        }
        public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
        {
            return GetMouseWorldPositionWithZ(Mouse.current.position.ReadValue(), worldCamera);
        }
        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
    }
}
