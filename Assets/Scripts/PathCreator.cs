using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [HideInInspector] public BezierPath path;

    public void CreatePath()
    {
        path = new BezierPath(transform.position);
    }
}
