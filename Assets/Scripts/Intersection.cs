using System;
using UnityEngine;

public class Intersection : MonoBehaviour
{
    [SerializeField] private Transform _p1;
    [SerializeField] private Transform _p2;
    [SerializeField] private Transform _p3;
    [SerializeField] private Transform _p4;

    public static bool IsIntersecting(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, out Vector3 intersection)
    {
        intersection = Vector2.zero;

        var d = (p2.x - p1.x) * (p4.y - p3.y) - (p2.y - p1.y) * (p4.x - p3.x);

        if (Math.Abs(d) <= 0)
        {
            return false;
        }

        var u = ((p3.x - p1.x) * (p4.y - p3.y) - (p3.y - p1.y) * (p4.x - p3.x)) / d;
        var v = ((p3.x - p1.x) * (p2.y - p1.y) - (p3.y - p1.y) * (p2.x - p1.x)) / d;

        if (u < 0.0f || u > 1.0f || v < 0.0f || v > 1.0f)
        {
            return false;
        }

        intersection.x = p1.x + u * (p2.x - p1.x);
        intersection.y = p1.y + u * (p2.y - p1.y);

        return true;
    }

    private void Update()
    {
        if (IsIntersecting(_p1.position, _p2.position, _p3.position, _p4.position, out var intersection))
        {
            transform.position = intersection;
        }
    }
}
