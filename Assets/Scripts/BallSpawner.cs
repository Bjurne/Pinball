using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab = default;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"spawn");
            SpawnBall();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(0f, 0.5f, 0.5f, 0.5f);
        Gizmos.DrawSphere(Vector3.zero, 0.2f);
    }

    internal void SpawnBall()
    {
        var go = Instantiate(ballPrefab, transform.position, Quaternion.identity, transform);
        var rb = go.GetComponent<Rigidbody>();
        var randomPower = UnityEngine.Random.Range(-0.1f, 0.1f);
        rb.AddRelativeForce(new Vector3(randomPower, 0f, 0f), ForceMode.Impulse);
    }
}
