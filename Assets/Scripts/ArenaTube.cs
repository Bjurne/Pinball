using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTube : MonoBehaviour, ICollisionReceiver
{
    [SerializeField] CollisionBroadcaster entranceTrigger = default;
    [SerializeField] CollisionBroadcaster exitTrigger = default;
    [SerializeField] Vector3 exitVelocity;

    public void OnTriggerEnterNotify(CollisionBroadcaster trigger, Collider other)
    {
        if (other.tag == "Player")
        {
            if (trigger == entranceTrigger)
            {
                StartCoroutine(TravelingSequence(other.gameObject));
            }
        }
    }

    public void OnTriggerStayNotify(CollisionBroadcaster trigger, Collider other)
    {
        
    }

    public void OnTriggerExitNotify(CollisionBroadcaster trigger, Collider other)
    {
        
    }

    private IEnumerator TravelingSequence(GameObject travellingBall)
    {
        travellingBall.SetActive(false);
        var rb = travellingBall.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(1f);
        //travellingBall.transform.position = exitTrigger.transform.position + (exitVelocity.normalized / 5f);
        travellingBall.transform.position = exitTrigger.transform.position;
        travellingBall.SetActive(true);
        if (rb != null)
            rb.AddForce(exitVelocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(exitTrigger.transform.position, exitTrigger.transform.position + exitVelocity.normalized);
    }
}
