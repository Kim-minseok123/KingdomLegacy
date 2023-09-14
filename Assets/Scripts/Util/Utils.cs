using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using static Define;

public class Utils
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>() ?? go.AddComponent<T>();
        return component;
    }
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;
        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;
        if (!recursive)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }
    //베지어 곡선
    public static Vector3 GetCurvePoint(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return (oneMinusT * oneMinusT * a) + (2f * oneMinusT * t * b) + (t * t * c);
    }

    /// <summary>
    /// Obtains the derivative of the curve (tangent)
    /// </summary>
    public static Vector3 GetCurveTangent(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        return 2f * (1f - t) * (b - a) + 2f * t * (c - b);
    }

    /// <summary>
    /// Obtains a direction perpendicular to the tangent of the curve
    /// </summary>
    public static Vector3 GetCurveNormal(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 tangent = GetCurveTangent(a, b, c, t);
        return Vector3.Cross(tangent, Vector3.forward);
    }
    public static void LookAt2D(Transform fromTransform, Vector3 toPosition)
    {
        Vector3 diff = toPosition - fromTransform.position;
        diff.Normalize();

        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        fromTransform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
