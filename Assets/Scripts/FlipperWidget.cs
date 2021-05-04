using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperWidget : RotatingWidget
{

    private void FixedUpdate()
    {
        if (IsPaused)
            return;

        #region SettingPivotRotation
        if (keyIsBeingPressed)
        {
            if (!RotationIsOverMaxLimit())
            {
                SetPivotRotation(widgetPivot.localEulerAngles.z + punchPower);
                rigidBody.AddRelativeTorque(Vector3.forward * punchPower, ForceMode.VelocityChange);
            }
            //rigidBody.AddRelativeTorque(new Vector3(0f, 0f, punchPower), ForceMode.VelocityChange);
            //rigidBody.angularVelocity = new Vector3(0f, 0f, 0f);
        }
        else if (!RotationIsLessThanMinLimit())
        {
            var backPower = punchPower;
            backPower *= -1f;
            backPower *= 0.5f;
            //rigidBody.AddRelativeTorque(new Vector3(0f, 0f, backPower));
            rigidBody.angularVelocity = Vector3.zero;
            SetPivotRotation(widgetPivot.localEulerAngles.z + backPower);
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
}
