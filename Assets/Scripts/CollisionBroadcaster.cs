using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBroadcaster : MonoBehaviour
{
    private ICollisionReceiver triggerReceiver;

    private void Start()
    {
        triggerReceiver = GetComponentInParent<ICollisionReceiver>();
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerReceiver.OnTriggerEnterNotify(this, other);
    }

    private void OnTriggerStay(Collider other)
    {
        triggerReceiver.OnTriggerStayNotify(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        triggerReceiver.OnTriggerExitNotify(this, other);
    }
}
