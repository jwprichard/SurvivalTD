using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public static class GameObjectUtilities
    {
        public static void DestroyAllChildren(this Transform t)
        {
            foreach (Transform child in t) Object.Destroy(child);
        }

        public static Quaternion LookAt(Vector3 p1, Vector3 p2)
        {

            float angle = 0;
            float opp;
            float adj;

            if (p1.x == p2.x)
            {
                //Looking down
                if (p1.y > p2.y)
                {
                    angle = 0;
                }
                //Looking up
                else
                {
                    angle = 180;
                }
            }

            else if (p1.y == p2.y)
            {
                //Looking right
                if (p1.x < p2.x)
                {
                    angle = 90;
                }
                //Looking left
                else
                {
                    angle = 270;
                }
            }
            else
            {
                if (p1.x < p2.x)
                {
                    //First quadrant angle +0
                    if (p1.y > p2.y)
                    {
                        angle = 0;
                        opp = Mathf.Abs(p1.x - p2.x);
                        adj = Mathf.Abs(p1.y - p2.y);
                    }
                    //Second quadrant angle +90
                    else
                    {
                        angle = 90;
                        adj = Mathf.Abs(p1.x - p2.x);
                        opp = Mathf.Abs(p1.y - p2.y);
                    }
                }
                //if (p1.x > p2.x)
                else
                {
                    //Third quadrant angle +180
                    if (p1.y <= p2.y)
                    {
                        angle = 180;
                        opp = Mathf.Abs(p1.x - p2.x);
                        adj = Mathf.Abs(p1.y - p2.y);
                    }
                    //Forth quadrant angle +270
                    else
                    {
                        angle = 270;
                        adj = Mathf.Abs(p1.x - p2.x);
                        opp = Mathf.Abs(p1.y - p2.y);
                    }
                }

                float a = Mathf.Atan(opp / adj) * 180f / Mathf.PI;
                angle += a;
            }

            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            return rotation;
        }
    }
}
