using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCollisionTrigger : MonoBehaviour
{
    private RotatingWidget parentWidget;
    private Vector3 pushVector;

    internal void Setup(RotatingWidget parentWidget, bool flipForward)
    {
        this.parentWidget = parentWidget;

        if (flipForward)
        {
            Debug.Log($"{parentWidget} {name} flipped!");

            FlipForward();
        }
    }

    internal void FlipForward()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -transform.localPosition.z);
        //transform.position.Set(transform.position.x, transform.position.y, -transform.position.z);
        //transform.Rotate(Vector3.forward, 180f);
        transform.localRotation = Quaternion.AngleAxis(180f, Vector3.up);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero, Vector3.forward * 10f);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (parentWidget != null && parentWidget.keyIsBeingPressed == false)
            return;

        if (otherCollider.tag == "Player")
        {
            var distanceFromWidgetPivot = Vector3.Distance(otherCollider.transform.position, parentWidget.widgetPivot.position);
            //pushVector = transform.forward * 0.4f;
            pushVector = transform.forward * (distanceFromWidgetPivot * 0.75f); //  Should probably be a smaller value, very long rotating widgets gets a too high value at their full reach ?
            var newPos = otherCollider.transform.position + pushVector;
            Debug.DrawLine(otherCollider.transform.position, newPos, Color.red, 5f);
            otherCollider.transform.SetPositionAndRotation(newPos, otherCollider.transform.rotation); // Other way to set position? The ball seems to stay at newPos for 2 frames
        }
    }
}
