using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomLineGenerator : ILineGenerator
{
    public Vector3[] Generate(int count, Rect rect, bool isCrossing)
    {
        List<Vector3> nodes = new List<Vector3>();
        for (int i = 0; i < Mathf.Min(3, count); ++i)
            nodes.Add(CreatePoint(rect));

        int n = 0;
        while (nodes.Count < count && n < 1024)
        {
            Vector2 newPoint = CreatePoint(rect);
            if (isCrossing)
            {
                nodes.Add(newPoint);
                continue;
            }

            if (!VectorIntersection.IsIntersecting(
                nodes.Select(v => new Vector2(v.x, v.y))
                .Concat(new List<Vector2> { newPoint })
                .ToList()))

                nodes.Add(newPoint);

            ++n;
        }

        return nodes.ToArray();
    }

    public Vector3 CreatePoint(Rect rect)
    {
        Vector2 pos = rect.min;
        pos.x += Random.value * rect.width;
        pos.y += Random.value * rect.height;

        return pos;
    }
}
