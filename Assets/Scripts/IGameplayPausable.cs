using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameplayPausable
{
    bool IsPaused { get; }

    void SetPaused(bool gameplayIsBeingPaused);
    //{
    //    if (rb == null)
    //    {
    //        Debug.LogWarning($"The Rigidbody of {this.gameObject.name} (GameplayPausable) wasn't set up properly");
    //        return;
    //    }

    //    if (gameplayIsBeingPaused)
    //    {
    //        originalVelocity = rb.velocity;
    //        originalAngularVelocity = rb.angularVelocity;
    //        rb.angularVelocity = Vector3.zero;
    //        rb.velocity = Vector3.zero;
    //        isPaused = true;
    //    }
    //    else
    //    {
    //        isPaused = false;
    //        rb.velocity = originalVelocity;
    //        rb.angularVelocity = originalAngularVelocity;
    //    }
    //}
}
