
using System.Collections.Generic;
using UnityEngine;

public abstract class VectorIntersection
{
    private static float VectorMult(Vector2 a, Vector2 b) => a.x * b.y - a.y * b.x;

    public static bool IsIntersecting(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        float v1 = VectorMult(p4 - p3, p1 - p3);
        float v2 = VectorMult(p4 - p3, p2 - p3);
        float v3 = VectorMult(p2 - p1, p3 - p1);
        float v4 = VectorMult(p2 - p1, p4 - p1);

        return (int)Mathf.Sign(v1 * v2) + (int)Mathf.Sign(v3 * v4) == -2;
    }

    public static bool IsIntersecting(List<Vector2> points)
    {
        for (int i = 0; i < points.Count - 1; ++i)
        {
            for (int j = 0; j < points.Count - 1; ++j)
            {
                if (i == j) continue;

                if (IsIntersecting(points[i], points[i + 1], points[j], points[j + 1]))
                    return true;
            }
        }
        return false;
    }
}
