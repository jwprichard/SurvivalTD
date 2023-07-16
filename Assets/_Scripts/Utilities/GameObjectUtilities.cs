using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public static class GameObjectUtilities
    {
        public static void DestroyAllChildren(this Transform t)
        {
            foreach (Transform child in t) Object.Destroy(child);
        }
    }
}
