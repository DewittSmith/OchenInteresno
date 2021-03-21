using UnityEngine;

public class LinePath : Path
{
    public override void UpdateNodes(Vector3[] nodes, bool isClosed)
    {
        Nodes = nodes;

        if (isClosed)
            AddPoint(nodes[0]);

        Points = Nodes;
    }
}
