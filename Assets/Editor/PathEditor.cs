using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor
{
    public int integer;
    PathCreator creator;
    BezierPath path;

    void AddSegment()
    {
        Vector3 newSegmentStartingPosition = path.GetLastAnchorPoint;
        Undo.RecordObject(creator, "Add segment");
        path.AddSegment(newSegmentStartingPosition);
    }

    void Input()
    {
        Event guiEvent = Event.current;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            AddSegment();
        }
    }

    void Draw()
    {
        for (int i = 0; i < path.NumberOfSegments; i++)
        {
            Vector3[] points = path.GetPointsInSegment(i);
            Handles.color = Color.black;
            Handles.DrawLine(points[1], points[0]);
            Handles.DrawLine(points[2], points[3]);
            Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.red, null, 40f);
        }

        Handles.color = Color.red;
        for (int i = 0; i < path.NumberOfPoints; i++)
        {
            var newPos = Handles.PositionHandle(path[i], Quaternion.identity);
            if (path[i] != newPos)
            {
                Undo.RecordObject(creator, "Move point");
                path.MovePoint(i, newPos);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Add segment"))
            path.AddSegment(path.GetLastAnchorPoint);

        if (GUILayout.Button("Remove last segment"))
            path.RemoveSegment();
    }

    private void OnSceneGUI()
    {
        Input();
        Draw();
    }

    void OnEnable()
    {
        creator = (PathCreator)target;
        if (creator.path == null)
        {
            creator.CreatePath();
        }
        path = creator.path;
    }
}
