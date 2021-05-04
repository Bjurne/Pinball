using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RotatingPlatformWidget : RotatingWidget
{
    private bool goingTowardsMaximumRotation;
    private bool isCurrentlyMoving;

    private void FixedUpdate()
    {

        #region SettingPivotRotation
        if (isCurrentlyMoving)
        {
            if (goingTowardsMaximumRotation && !RotationIsOverMaxLimit())
            {
                SetPivotRotation(widgetPivot.localEulerAngles.z + punchPower);
            }
            else if (!goingTowardsMaximumRotation && !RotationIsLessThanMinLimit())
            {
                var backPower = -punchPower;
                SetPivotRotation(widgetPivot.localEulerAngles.z + backPower);
            }
            else
            {
                isCurrentlyMoving = false;
            }
        }
        #endregion SettingPivotRotation

        #region UseRigidBodyAngularVelocity
        //if (Input.GetKey(key))
        //{
        //    if (!RotationIsOverMaxLimit())
        //    //rigidBody.AddRelativeTorque(new Vector3(0f, 0f, punchPower), ForceMode.VelocityChange);
        //    {
        //        rigidBody.AddRelativeTorque(new Vector3(0f, 0f, punchPower * Time.deltaTime));
        //    }
        //    //rigidBody.AddRelativeTorque(Vector3.forward * punchPower, ForceMode.VelocityChange);
        //}
        //else if (!RotationIsLessThanMinLimit())
        //{
        //    var backPower = punchPower;
        //    backPower *= -1f;
        //    backPower *= 0.5f;
        //    //rigidBody.AddRelativeTorque(new Vector3(0f, 0f, backPower));
        //    Debug.Log($"LESS THAN MIN, adding {backPower}");
        //    rigidBody.AddRelativeTorque(new Vector3(0f, 0f, backPower * Time.deltaTime));
        //}
        #endregion UseRigidBodyAngularVelocity

        //Clamp Rotation

        if (RotationIsOverMaxLimit())
        {
            SetPivotRotation(maxRotation);
        }
        else if (RotationIsLessThanMinLimit())
        {
            SetPivotRotation(minRotation);
        }
    }

    internal override void OnButtonPressed(GameplayAction key)
    {
        if (key == this.key)
        {
            goingTowardsMaximumRotation = !goingTowardsMaximumRotation;
            isCurrentlyMoving = true;
        }
    }

    internal override void OnButtonReleased(GameplayAction key)
    {
        //if (key == this.key)
        //{
        //    keyIsBeingPressed = false;
        //}
    }

#if UNITY_EDITOR
    internal override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(Vector3.zero, new Vector3(2f, 0f, 0f));
        Handles.matrix = transform.localToWorldMatrix;
        Handles.color = new Color(1f, 0f, 0f, 0.25f);
        //Handles.DrawSolidArc(Vector3.zero, Vector3.forward, Vector3.right, angle, 1f);
        //var mirroredAngle = maxRotation;
        Handles.DrawSolidArc(Vector3.zero, Vector3.forward, Vector3.left, angle, 1f);
    }
#endif
}
