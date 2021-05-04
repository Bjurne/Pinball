using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaPropsSet : MonoBehaviour
{
    private BallSpawner ballSpawner;

    private void OnEnable()
    {
        ballSpawner = GetComponentInChildren<BallSpawner>();
    }

    internal void SpawnBall()
    {
        ballSpawner.SpawnBall();
    }

    internal void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
