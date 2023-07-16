using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public static class GameObjectUtilities
    {
        public static void DestroyAllChildren(this Transform t)
        {
            foreach (Transform child in t) Object.Destroy(child);
        }

        public static Transform FindTransformInDistance(Transform startPosition, int layer, float range)
        {
            return FindTransformInDistance(startPosition.position, layer, range);
        }

        public static Transform FindTransformInDistance(Vector2 startPosition, int layer, float range)
        {
            float minDistance = int.MaxValue;
            Vector2 position = startPosition;
            Transform target = null;

            Collider2D[] collisions = Physics2D.OverlapCircleAll(position, range, 1 << layer);
            if (collisions.Length > 0)
            {
                foreach (Collider2D collision in collisions)
                {

                    float newDistance = Vector2.Distance(position, collision.transform.position);
                    if (newDistance < minDistance)
                    {
                        minDistance = newDistance;
                        target = collision.transform;
                    }
                }
            }

            return target;

        }
    }

}
