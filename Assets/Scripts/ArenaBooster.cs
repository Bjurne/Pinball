using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBooster : MonoBehaviour
{
    [SerializeField] Vector3 boostVector = default;
    private List<Rigidbody> ballRigidbodys;

    private void Awake()
    {
        ballRigidbodys = new List<Rigidbody>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < ballRigidbodys.Count; i++)
        {
            ballRigidbodys[i].velocity += boostVector.normalized;
        }
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.tag == "Player")
        {
            ballRigidbodys.Add(otherCollider.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.tag == "Player")
        {
            ballRigidbodys.Remove(otherCollider.attachedRigidbody);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawLine(Vector3.zero, boostVector);
    }
#if UNITYEDITOR
    
#endif
}
