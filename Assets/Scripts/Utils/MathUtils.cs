using UnityEngine;

namespace Utils
{
    public static class MathUtils
    {
        public static Vector3 Add(this Vector3 v3, Vector2 v2)
        {
            return new Vector3(v3.x + v2.x, v3.y + v2.y, v3.z);
        }
        
        // shamlessly stolen from freya holmer's talk
        // https://youtu.be/LSNQuFEDOyQ
        public static float Decay(this float a, float b, float decay, float dt)
        {
            return b+(a-b)*Mathf.Exp(-decay*dt);
        }

        public static Vector2 Decay(this Vector2 a, Vector2 b, float decay, float dt)
        {
            return new Vector2(Decay(a.x, b.x, decay, dt), Decay(a.y, b.y, decay, dt));
        }

        public static Vector3 Decay(this Vector3 a, Vector3 b, float decay, float dt)
        {
            return new Vector3(Decay(a.x, b.x, decay, dt), Decay(a.y, b.y, decay, dt), Decay(a.z, b.z, decay, dt));
        }
    }
}