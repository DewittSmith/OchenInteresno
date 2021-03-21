using UnityEngine;

public interface ILineGenerator
{
    Vector3[] Generate(int count, Rect rect, bool isCrossing);
    Vector3 CreatePoint(Rect rect);
}
