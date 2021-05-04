using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayActionButton : MonoBehaviour
{
    [SerializeField] private GameplayAction action = default;
    [SerializeField] internal Image symbolImage = default;
    [SerializeField] private bool manuallySetSymbolSize = default;
    internal GameplayAction Action { get => action; }

    private void Start()
    {
        Invoke("Setup", 1f);
    }

    internal void Setup()
    {
        UpdateButtonSprite();
        if (!manuallySetSymbolSize)
        {
            var rectSizeDelta = GetComponent<RectTransform>().sizeDelta;
            symbolImage.rectTransform.sizeDelta = rectSizeDelta / 1.4f;
        }
    }

    internal void SetAction(GameplayAction newAction)
    {
        action = newAction;
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        symbolImage.sprite = GameLocalisation.Instance.GetGameplayActionSprite(action);
        symbolImage.color = GameLocalisation.Instance.GetGameplayActionColor(action);
    }

    public void OnButtonPressed()
    {
        ArenaControlBoard.Instance.BroadcastButtonPressed(action);
    }

    public void OnButtonReleased()
    {
        ArenaControlBoard.Instance.BroadcastButtonReleased(action);
    }
}
