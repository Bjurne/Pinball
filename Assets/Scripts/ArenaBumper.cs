using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBumper : MonoBehaviour
{
    void OnTriggerEnter(Collider otherCollider)
    {
        //if (otherCollider.tag == "Player")
        //{
        //    var rb = otherCollider.GetComponent<Rigidbody>();
        //    rb.velocity = (rb.velocity * -2f);
        //}

        if (otherCollider.tag == "Player")
        {
            var incommingVelocity = otherCollider.attachedRigidbody.velocity; // Better if we normalize this? (= less focus on ball velocity, more dependent on where on the bumper it hits)
            //incommingVelocity.z = 0.5f; // <----- directs the ball "into the arena floor", is there a risk for ball bouncing back up and out of bounds?
            var flatBumperDirectionOut = transform.up;
            var otherPos = otherCollider.gameObject.transform.position;
            var relativeOffset = (otherPos - transform.position) / 2f; // Where on the bumper does the ball hit? Scaled down - not as important as "bumper.up"
            var bumperOutVector = flatBumperDirectionOut + relativeOffset;
            //var flatDirection = (otherPos - (transform.position + offsetBumperPos)).normalized;


            //var outDirection = Vector3.Scale(-incommingVelocity, bumperOutVector).normalized;
            var outDirection = ((-incommingVelocity) + bumperOutVector).normalized;

            otherCollider.attachedRigidbody.velocity = outDirection * 5f;

            Debug.DrawLine(transform.position, transform.position + flatBumperDirectionOut, Color.red, 5f);
        }
    }

    void OnTriggerExit(Collider otherCollider)
    {
        //if (otherCollider.tag == "Player")
        //{
        //    otherCollider.attachedRigidbody.velocity *= 2f;
        //}
    }
}
