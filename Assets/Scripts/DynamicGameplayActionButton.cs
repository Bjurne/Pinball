using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGameplayActionButton : GameplayActionButton
{
    [SerializeField] List<GameObject> changeActionButtons = default;
    [SerializeField] List<GameplayAction> availableActions = default;
    [SerializeField] TMPro.TextMeshProUGUI fieldActionName = default;
    private Coroutine showSequence;
    //private List<ChangeActionButton> changeActionButtonWrappers;


    private class ChangeActionButton : Component // TODO: break these into proper child widgets, 
    {
        private Transform button;
        internal ChangeActionButton(Transform button)
        {
            this.button = button;
        }

        internal void SetAction(GameplayAction action)
        {
            var image = button.GetComponentsInChildren<Image>(true)[1];
            image.sprite = GameLocalisation.Instance.GetGameplayActionSprite(action);
            image.color = GameLocalisation.Instance.GetGameplayActionColor(action);
        }
    }

    private void Start()
    {
        //changeActionButtonWrappers = new List<ChangeActionButton>();
        fieldActionName.text = Action.ToString();
        List<GameObject> buttonsToRemoveFromList = new List<GameObject>();
        for (int i = 0; i < changeActionButtons.Count; i++)
        {
            if (i > availableActions.Count)
            {
                Debug.Log($"Action not listed, hiding choice");
                changeActionButtons[i].SetActive(false);
                buttonsToRemoveFromList.Add(changeActionButtons[i]);
            }
            var button = new ChangeActionButton(changeActionButtons[i].transform);
            button.SetAction(availableActions[i]);
            //var image = changeActionButtons[i].GetComponentInChildren<Image>();
            //image.sprite = GameLocalisation.Instance.GetGameplayActionSprite(availableActions[i]);
        }
        for (int i = 0; i < buttonsToRemoveFromList.Count; i++)
        {
            changeActionButtons.Remove(buttonsToRemoveFromList[i]);
        }

        HideButtons();
        base.Setup();
    }

    public new void OnButtonPressed()
    {
        base.OnButtonPressed();
        showSequence = StartCoroutine(ShowButtons());
    }

    public new void OnButtonReleased()
    {
        base.OnButtonReleased();
        StopCoroutine(showSequence);
        HideButtons();
        //ArenaControlBoard.Instance.BroadcastButtonReleased(action);
    }

    public void ChangeButtonAction(int newActionIndex)
    {
        base.OnButtonReleased();
        //var button = GetComponent<Button>();
        //button.Select();
        //action = (GameplayAction)newActionIndex;
        SetAction((GameplayAction)newActionIndex);
        fieldActionName.text = Action.ToString();
        //base.OnButtonPressed();
        //HideButtons();
    }

    private IEnumerator ShowButtons()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < changeActionButtons.Count; i++)
        {
            changeActionButtons[i].SetActive(true);
        }

        yield return null;
    }

    private void HideButtons()
    {
        for (int i = 0; i < changeActionButtons.Count; i++)
        {
            changeActionButtons[i].SetActive(false);
        }
    }
}
