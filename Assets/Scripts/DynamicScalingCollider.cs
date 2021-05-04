using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicScalingCollider : MonoBehaviour
{
    [SerializeField] private float minimumColliderRadius = 0.5f;
    [SerializeField] private float maximumColliderRadius = 0.8f;
    [SerializeField] GameObject energyMesh = default;
    private Rigidbody rb;
    private new SphereCollider collider;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > 2f && collider.radius < maximumColliderRadius)
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z);
            collider.radius = maximumColliderRadius;
            GetComponentInParent<MeshRenderer>().material.color = Color.red; // Just for debug
            energyMesh.SetActive(true);
        }
        else if (rb.velocity.magnitude < 1f && collider.radius > minimumColliderRadius)
        {
            collider.radius = minimumColliderRadius;
            //transform.position = new Vector3(transform.position.x, transform.position.y - 0.15f, transform.position.z);
            GetComponentInParent<MeshRenderer>().material.color = Color.green; // Just for debug
            energyMesh.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (collider.radius > minimumColliderRadius)
        //{
        //    collider.radius = minimumColliderRadius;
        //}
    }
}
