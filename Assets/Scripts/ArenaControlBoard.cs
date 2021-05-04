using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArenaControlBoard : MonoBehaviour
{
    #region Singleton
    public static ArenaControlBoard Instance { get { return instance; } }
    private static ArenaControlBoard instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    #endregion Singleton

    public Action<GameplayAction> OnBroadcastButtonPressed;
    public Action<GameplayAction> OnBroadcastButtonReleased;

    internal void BroadcastButtonPressed(GameplayAction key)
    {
        OnBroadcastButtonPressed?.Invoke(key);
    }

    internal void BroadcastButtonReleased(GameplayAction key)
    {
        OnBroadcastButtonReleased?.Invoke(key);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            BroadcastButtonPressed(GameplayAction.LeftMain);
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            BroadcastButtonReleased(GameplayAction.LeftMain);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            BroadcastButtonPressed(GameplayAction.RightMain);
        if (Input.GetKeyUp(KeyCode.RightArrow))
            BroadcastButtonReleased(GameplayAction.RightMain);

        if (Input.GetKeyDown(KeyCode.Z))
            BroadcastButtonPressed(GameplayAction.LeftSpecial);
        if (Input.GetKeyUp(KeyCode.Z))
            BroadcastButtonReleased(GameplayAction.LeftSpecial);

        if (Input.GetKeyDown(KeyCode.X))
            BroadcastButtonPressed(GameplayAction.RightSpecial);
        if (Input.GetKeyUp(KeyCode.X))
            BroadcastButtonReleased(GameplayAction.RightSpecial);
    }
}

public enum GameplayAction
{
    DebugKey = -1,

    LeftMain = 0,
    RightMain = 1,
    LeftSpecial = 2,
    RightSpecial = 3,

    //ActivateMagneticField = 20,
    //DeactivateMagneticField = 21,

    ArenaPause = 30,
    ArenaNext = 31,
    ArenaPrevious = 32,

    ArenaShake = 35,

    DebugSpawnBall = 60,
}
