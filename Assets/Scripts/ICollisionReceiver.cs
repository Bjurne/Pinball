using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionReceiver
{
    void OnTriggerEnterNotify(CollisionBroadcaster trigger, Collider other);
    void OnTriggerStayNotify(CollisionBroadcaster trigger, Collider other);
    void OnTriggerExitNotify(CollisionBroadcaster trigger, Collider other);
}