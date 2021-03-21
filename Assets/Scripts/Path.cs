using System.Linq;
using UnityEngine;

public abstract class Path
{
    public Vector3[] Points { get; protected set; } = new Vector3[0];
    public Vector3[] Nodes { get; protected set; } = new Vector3[0];

    public float GetPathLength()
    {
        float length = 0;
        for (int i = 0; i < Points.Length - 1; ++i)
            length += Vector2.Distance(Points[i], Points[i + 1]);
        return length;
    }

    public float GetSegmentLength(int i)
    {
        if (i < Points.Length - 1) return Vector2.Distance(Points[i], Points[i + 1]);
        else return 1;
    }

    public void AddPoint(Vector3 point)
    {
        Nodes = Nodes.Concat(new[] { point }).ToArray();
        UpdateNodes(Nodes, false);
    }

    public abstract void UpdateNodes(Vector3[] nodes, bool isClosed);

    public Vector2 Interpolate(ref float t, ref int i, bool loop)
    {
        if (t >= 1)
        {
            t = 0;
            if (loop) i = (i + 1) % (Points.Length - 1);
            else i = Mathf.Min(i + 1, Points.Length - 1);
        }

        if (i + 1 <= Points.Length - 1) return Vector2.Lerp(Points[i], Points[i + 1], t);
        else return Points[i];
    }
}