using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuActionButton : UIWidget
{
    [SerializeField] MenuAction menuAction = default;

    private Button button;


    private void Start()
    {
        Setup();
        button = GetComponent<Button>();
    }

    public void OnButtonPressed()
    {
        Debug.Log($"{menuAction}");
        //RootMenu.Instance.OnButtonPressed(menuAction);
    }
}

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIWidget : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private bool isActive;
    private Coroutine transitionRoutine;

    internal void Setup()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    internal void SetActive(bool value)
    {
        if (canvasGroup == null)
            return;

        if (transitionRoutine != null)
        {
            StopCoroutine(transitionRoutine);
            transitionRoutine = null;
        }
        canvasGroup.alpha = value ? 1 : 0;
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;

        isActive = value;
    }

    internal void ShowSequence()
    {
        //if (isActive)
        //    return;
        if (transitionRoutine != null)
            StopCoroutine(transitionRoutine);
        transitionRoutine = StartCoroutine(Transition(true, 0.2f));
    }

    internal void HideSequence()
    {
        //if (!isActive)
        //    return;
        if (transitionRoutine != null)
            StopCoroutine(transitionRoutine);
        transitionRoutine = StartCoroutine(Transition(false, 0.2f));
    }

    private IEnumerator Transition(bool transitionIn, float duration)
    {
        var targetAlpha = transitionIn ? 1f : 0f;
        var originalAlpha = transitionIn ? 0f : 1f;
        var originalDuration = duration;

        Debug.Log($"{transitionIn} - {originalAlpha} -> {targetAlpha}");

        SetActive(!transitionIn);

        while (duration > 0f)
        {
            duration = duration - Time.unscaledDeltaTime;
            var lerpValue = duration / originalDuration;
            canvasGroup.alpha = Mathf.Lerp(targetAlpha, originalAlpha, lerpValue);
            yield return new WaitForEndOfFrame();
        }

        SetActive(transitionIn);
        transitionRoutine = null;
    }
}

public enum MenuAction
{
    DebugMenuAction = 0,
    ResumeGame = 1,
    Settings = 2,
    BehindTheScenes = 3,

    Back = 8,
    Quit = 9,

    SettingsToggleSoundFX = 20,
    SettingsToggleMusic = 21,

}
