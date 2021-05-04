using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBallSpotlight : MonoBehaviour
{
    private Transform gameBall;
    private void Start()
    {
        gameBall = GetComponentInParent<GameBall>().transform;
        transform.SetParent(Camera.main.transform);
        transform.localPosition = Vector3.forward * 4f;
    }

    private void FixedUpdate()
    {
        if (gameBall != null)
            transform.LookAt(gameBall);
        else
            Destroy(gameObject);
    }
}
