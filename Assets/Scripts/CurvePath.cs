using System.Collections.Generic;
using UnityEngine;

public class CurvePath : Path
{
    private const float curvature = 0.8f;
    private const int iterations = 4;

    public override void UpdateNodes(Vector3[] nodes, bool isClosed)
    {
        Nodes = nodes;
        if (nodes.Length <= 2)
        {
            Points = nodes;
            return;
        }

        List<Vector3> path = new List<Vector3>(nodes);
        if (isClosed) path.Add(nodes[0]);

        for (int i = 0; i < iterations; ++i)
        {
            List<Vector3> temp = new List<Vector3> { path[0] };
            for (int j = 0; j < path.Count - 2; ++j)
            {
                Vector2 p0 = Vector2.Lerp(path[j], path[j + 1], curvature);
                Vector2 p1 = Vector2.Lerp(path[j + 2], path[j + 1], curvature);

                temp.Add(p0);
                temp.Add(p1);
            }
            temp.Add(path[path.Count - 1]);
            path = temp;
        }
        Points = path.ToArray();
    }
}
