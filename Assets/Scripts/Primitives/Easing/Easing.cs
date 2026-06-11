using System;
using UnityEngine;

namespace Primitives.Easing
{
    public static class Easing
    {
        public static float CalcLinear(float t)
        {
            return t;
        }

        public static float CalcEaseInQuad(float t)
        {
            return t * t;
        }

        public static float CalcEaseOutQuad(float t)
        {
            return t * (2 - t);
        }

        public static float CalcEaseInOutQuad(float t)
        {
            if (t < 0.5f) return 2 * t * t;
            return -1 + (4 - 2 * t) * t;
        }

        public static float CalcEaseInCubic(float t)
        {
            return t * t * t;
        }

        public static float CalcEaseOutCubic(float t)
        {
            return 1 - Mathf.Pow(1 - t, 3);
        }

        public static float CalcEaseInOutCubic(float t)
        {
            if (t < 0.5) return 4 * t * t * t;
            return 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
        }

        public static float CalcEaseInSine(float t)
        {
            return 1 - Mathf.Cos(t * Mathf.PI / 2);
        }
        public static float CalcEaseOutSine(float t)
        {
            return Mathf.Sin(t * Mathf.PI / 2);
        }
        public static float CalcEaseInOutSine(float t)
        {
            return -(Mathf.Cos(t * Mathf.PI) - 1) / 2;
        }
        public static float CalcEaseInCirc(float t)
        {
            return 1 - Mathf.Sqrt(1 - Mathf.Pow(t, 2));
        }
        public static float CalcEaseOutCirc(float t)
        {
            return Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
        }
        public static float CalcEaseInOutCirc(float t)
        {
            if (t < 0.5) return (1 - MathF.Sqrt(1 - Mathf.Pow(2 * t, 2))) / 2;
            return (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) / 2;
        }
        public static float CalcEaseInElastic(float t)
        {
            float c = 2 * Mathf.PI / 3;
            if (t == 0) return 0;
            if (t == 1) return 1;
            return -Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((t * 10f - 10.75f) * c);
        }
        public static float CalcEaseOutElastic(float t)
        {
            float c = 2 * Mathf.PI / 3;
            if (t == 0) return 0;
            if (t == 1) return 1;
            return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * c) + 1f;
        }
        public static float CalcEaseInOutElastic(float t)
        {
            float c = 2 * Mathf.PI / 4.5f;
            if (t == 0) return 0;
            if (t == 1) return 1;
            if (t < 0.5) return -Mathf.Pow(2f, 20f * t - 10f) * Mathf.Sin((20 * t - 11.125f) * c) / 2;
            return Mathf.Pow(2f, -20f * t + 10) * Mathf.Sin((20 * t - 11.125f) * c) / 2 + 1f;
        }
        public static float CalcEaseInBack(float t)
        {
            float c1 = 1.70158f;
            float c2 = c1 + 1;
            return c2 * t * t * t - c1 * t * t;
        }
        public static float CalcEaseOutBack(float t)
        {
            float c1 = 1.70158f;
            float c2 = c1 + 1;
            return 1 + c2 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
        }
        public static float CalcEaseInOutBack(float t)
        {
            float c1 = 1.70158f;
            float c2 = c1 + 1.525f;
            if (t < 0.5) return MathF.Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2) / 2;
            else return Mathf.Pow(2 * t - 2, 2) * ((c2 + 1) * (t * 2 - 2) + c2 + 2) / 2;
        }
        public static float CalcEaseInBounce(float t)
        {
            return 1 - CalcEaseOutBounce(1 - t);
        }
        public static float CalcEaseOutBounce(float t)
        {
            float c1 = 7.5625f;
            float c2 = 2.75f;
            if (t < 1 / c2)
            {
                return c1 * t * t;
            }
            else if (t < 2 / c2)
            {
                return c1 * (t -= 1.5f / c2) * t + 0.75f;
            }
            else if (t < 2.5 / c2)
            {
                return c1 * (t -= 2.25f / c2) * t + 0.9375f;
            }
            else
            {
                return c1 * (t -= 2.625f / c2) * t + 0.984375f;
            }
        }
        public static float CalcEaseInOutBounce(float t)
        {
            if (t < 0.5) return (1 - CalcEaseOutBounce(1 - 2 * t)) / 2;
            return (1 + CalcEaseOutBounce(2 * t - 1)) / 2;
        }
    }
}