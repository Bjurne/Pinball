using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BezierPath
{
    [SerializeField, HideInInspector] List<Vector3> points;

    public int NumberOfPoints { get { return points.Count; } }
    public int NumberOfSegments { get { return (points.Count - 4) / 3 + 1; } }

    public Vector3 GetLastAnchorPoint { get { return points[points.Count - 1]; } }

    public Vector3 this[int i] { get { return points[i]; } }

    public BezierPath(Vector3 centre)
    {
        points = new List<Vector3>
        {
            centre + Vector3.left,
            centre + (Vector3.left + Vector3.up) * 0.5f,
            centre + (Vector3.right + Vector3.down) * 0.5f,
            centre + Vector3.right
        };
    }

    public void AddSegment(Vector3 anchorPos)
    {
        points.Add(points[points.Count - 1] * 2f - points[points.Count - 2]);
        points.Add(points[points.Count - 1] + anchorPos * 0.5f);
        points.Add(anchorPos);
    }

    public Vector3[] GetPointsInSegment(int i)
    {
        return new Vector3[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[i * 3 + 3] };
    }

    public void MovePoint(int i, Vector3 newPos)
    {
        Vector3 deltaMove = newPos - points[i];
        points[i] = newPos;

        if (i % 3 == 0)
        {
            if (i + 1 < points.Count)
                points[i + 1] += deltaMove;
            if (i - 1 >= 0)
                points[i - 1] += deltaMove;
        }
        else
        {
            var nextPointIsAnchorPoint = (i + 1) % 3 == 0;
            var correspondingControlIndex = (nextPointIsAnchorPoint) ? i + 2 : i - 2;
            var anchorIndex = (nextPointIsAnchorPoint) ? i + 1 : i - 1;

            if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count)
            {
                var distance = (points[anchorIndex] - points[correspondingControlIndex]).magnitude;
                var direction = (points[anchorIndex] - newPos).normalized;
                points[correspondingControlIndex] = points[anchorIndex] + direction * distance;
            }
        }
    }

    public void RemoveSegment()
    {
        if (NumberOfSegments <= 1)
            return;

        var firstIndex = points.Count - 1;
        var secondIndex = points.Count - 2;
        var thirdIndex = points.Count - 3;

        if (firstIndex >= 0)
            points.RemoveAt(firstIndex);
        if (secondIndex >= 0)
            points.RemoveAt(secondIndex);
        if (thirdIndex >= 0)
            points.RemoveAt(thirdIndex);
    }
}
