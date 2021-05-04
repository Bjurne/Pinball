using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaPickup : MonoBehaviour
{
    [SerializeField] PickupType pickupType = default;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Arena.Instance.PickupCollected(pickupType);
            Destroy(gameObject);
        }
    }
}

public enum PickupType
{
    DefaultGameBall = 0,
}
