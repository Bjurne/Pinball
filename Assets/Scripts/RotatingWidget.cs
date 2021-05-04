using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class RotatingWidget : PlayerControlledWidget
{
    [SerializeField, Range(0, 359)] float startRotation;
    internal float minRotation;
    [SerializeField, Range(-300, 300)] internal float maxRotation;
    [SerializeField] internal float punchPower;
    internal float angle = 45;


    private void Start()
    {
        Setup();
    }

    private new void Setup()
    {
        base.Setup();
        rigidBody.centerOfMass = widgetPivot.transform.localPosition;
        rigidBody.maxAngularVelocity = 14f;
        //if (startRotation >= maxRotation)
        //    punchPower *= -1;
        if (maxRotation < 0f)
            punchPower *= -1;


        var forceCollisionTrigger = GetComponentInChildren<ForceCollisionTrigger>();
        var flipForward = punchPower < 0f;
        if (forceCollisionTrigger != null)
        {
            forceCollisionTrigger.Setup(this, flipForward);
        }
    }

    internal void SetPivotRotation(float value)
    {
        widgetPivot.localRotation = Quaternion.Euler(0f, 0f, value);
    }

    private bool RotationIsWithinAllowedLimits()
    {
        return RotationIsOverMaxLimit() == false && RotationIsLessThanMinLimit() == false;
    }

    internal bool RotationIsOverMaxLimit()
    {
        var pivotRotation = (widgetPivot.eulerAngles.z - startRotation + 90f) % 360;
        if (pivotRotation == 0f)
            return false;

        if (punchPower > 0)
        {
            if (pivotRotation > 315f)
                pivotRotation -= 360f;
            return pivotRotation > maxRotation;
        }
        else
        {
            if (pivotRotation > 45f)
                pivotRotation -= 360f;
            return pivotRotation < maxRotation;
        }
    }

    internal bool RotationIsLessThanMinLimit()
    {
        var pivotRotation = (widgetPivot.eulerAngles.z - startRotation + 90f) % 360;
        if (pivotRotation == 0f)
            return false;

        if (punchPower > 0)
        {
            if (pivotRotation > 315f)
                pivotRotation -= 360f;

            return pivotRotation < minRotation;
        }
        else
        {
            //if (pivotRotation < -315f)
            //    pivotRotation += 360f;
            if (pivotRotation > 45f)
                pivotRotation -= 360f;
            return pivotRotation > minRotation;
        }
    }

    internal override void OnEnable()
    {
        base.OnEnable();

        if (initialized)
        {
            //var rotation = Quaternion.Euler(66f, 0f, startRotation - 90f);
            //transform.rotation = rotation;
            SetPivotRotation(minRotation);
            keyIsBeingPressed = false;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        var rotation = Quaternion.Euler(66f, 0f, startRotation - 90f);
        transform.rotation = rotation;
        minRotation = 0f;
    }

    internal virtual void OnDrawGizmosSelected()
    {
        Handles.matrix = transform.localToWorldMatrix;
        Handles.color = new Color(1f, 0f, 0f, 0.25f);
        angle = maxRotation;
        Handles.DrawSolidArc(Vector3.zero, Vector3.forward, Vector3.right, angle, 1f);
    }
#endif
}
