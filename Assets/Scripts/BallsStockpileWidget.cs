using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsStockpileWidget : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI numberOfBallsField = default;
    private int numberOfBallsInStockpile;

    private void Start()
    {
        UIEvents.Instance.OnStockpileUpdated += OnStockpileUpdated;
    }

    private void OnDestroy()
    {
        UIEvents.Instance.OnStockpileUpdated -= OnStockpileUpdated;
    }

    private void OnStockpileUpdated(int newAmount)
    {
        numberOfBallsInStockpile = newAmount;
        numberOfBallsField.text = $": {numberOfBallsInStockpile}";
    }
}
