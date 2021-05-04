using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{

    #region Singleton
    public static UIEvents Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion Singleton

    public Action OnArenaAdvanced;

    public Action<int> OnStockpileUpdated;

}
