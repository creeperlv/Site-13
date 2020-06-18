// RotationTools.cs
// Created by Alexander Ameye
// Version 1.1.0

using System.Collections;
using UnityEngine;

namespace AlexDoorSystem
{
    public class RotationTools : MonoBehaviour
    {
        public static IEnumerator Rotate(GameObject door, float StartAngle, float EndAngle, float Speed, bool UnityConvention, float Offset, bool ShortestWay)
        {
            Quaternion StartRotation, EndRotation, RotationOffset;

            RotationOffset = Quaternion.Euler(0, Offset, 0);

            AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

            float TimeProgression = 0f;

            if (UnityConvention) // Unity Convention
            {
                StartRotation = Quaternion.Euler(0, StartAngle, 0);
                EndRotation = Quaternion.Euler(0, EndAngle, 0);
            }

            else // Alex's Door System Convention
            {
                StartRotation = Quaternion.Euler(0, -StartAngle, 0);
                EndRotation = Quaternion.Euler(0, -EndAngle, 0);
            }

            while (TimeProgression <= (1 / Speed))
            {
                TimeProgression += Time.deltaTime;
                float RotationProgression = Mathf.Clamp01(TimeProgression / (1 / Speed));
                float RotationCurveValue = curve.Evaluate(RotationProgression);

                door.transform.rotation = Lerp(StartRotation * RotationOffset, EndRotation * RotationOffset, RotationCurveValue, ShortestWay);

                yield return null;
            }
        }

        public static Quaternion Lerp(Quaternion p, Quaternion q, float t, bool shortWay)
        {
            if (shortWay)
            {
                float dot = Quaternion.Dot(p, q);
                if (dot < 0.0f)
                    return Lerp(ScalarMultiply(p, -1.0f), q, t, true);
            }

            Quaternion r = Quaternion.identity;
            r.x = p.x * (1f - t) + q.x * (t);
            r.y = p.y * (1f - t) + q.y * (t);
            r.z = p.z * (1f - t) + q.z * (t);
            r.w = p.w * (1f - t) + q.w * (t);
            return r;
        }

        public static Quaternion Slerp(Quaternion p, Quaternion q, float t, bool shortWay)
        {
            float dot = Quaternion.Dot(p, q);
            if (shortWay)
            {
                if (dot < 0.0f)
                    return Slerp(ScalarMultiply(p, -1.0f), q, t, true);
            }

            float angle = Mathf.Acos(dot);
            Quaternion first = ScalarMultiply(p, Mathf.Sin((1f - t) * angle));
            Quaternion second = ScalarMultiply(q, Mathf.Sin((t) * angle));
            float division = 1f / Mathf.Sin(angle);
            return ScalarMultiply(Add(first, second), division);
        }

        public static Quaternion ScalarMultiply(Quaternion input, float scalar)
        {
            return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
        }

        public static Quaternion Add(Quaternion p, Quaternion q)
        {
            return new Quaternion(p.x + q.x, p.y + q.y, p.z + q.z, p.w + q.w);
        }
    }
}