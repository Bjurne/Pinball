using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GameBall : MonoBehaviour, IGameplayPausable
{
    //[SerializeField] Transform arenaFloor = default;

    //private void FixedUpdate()
    //{
    //    var yPos = transform.position.y;
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
    //    {
    //        yPos = hit.point.y;
    //        Debug.DrawLine(transform.position, hit.point);
    //        var xPos = transform.position.x;
    //        var zPos = transform.position.z;

    //        transform.position = new Vector3(xPos, yPos, zPos);
    //    }
    //}

    private Rigidbody rb;
    private bool isPaused;
    private Vector3 originalVelocity;
    private Vector3 originalAngularVelocity;

    public bool IsPaused => isPaused;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetPaused(bool gameplayIsBeingPaused)
    {
        if (rb == null)
        {
            Debug.LogWarning($"The Rigidbody of {this.gameObject.name} (IGameplayPausable) wasn't set up properly");
            return;
        }

        //if (gameplayIsBeingPaused)
        //    rb.constraints = RigidbodyConstraints.FreezeAll;
        //else
        //    rb.constraints = RigidbodyConstraints.None;

        if (gameplayIsBeingPaused)
        {
            originalVelocity = rb.velocity;
            originalAngularVelocity = rb.angularVelocity;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            isPaused = true;
        }
        else
        {
            isPaused = false;
            rb.constraints = RigidbodyConstraints.None;
            rb.velocity = originalVelocity;
            rb.angularVelocity = originalAngularVelocity;
        }
    }
}
