using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPresenterView : UIWidget
{
    private List<UIWidget> widgets;

    void Start()
    {
        Setup();
        widgets = new List<UIWidget>();
        foreach (UIWidget widget in GetComponentsInChildren<UIWidget>())
        {
            if (widget != this)
                widgets.Add(widget);
        }
    }

    internal new void SetActive(bool value)
    {
        StopAllCoroutines();
        base.SetActive(value);
        for (int i = 0; i < widgets.Count; i++)
        {
            widgets[i].SetActive(value);
        }
    }

    internal new void ShowSequence()
    {
        base.ShowSequence();
        if (widgets.Count > 0)
            StartCoroutine(ShowWidgetsSequence());
    }

    internal new void HideSequence()
    {
        base.HideSequence();
        if (widgets.Count > 0)
            StartCoroutine(HideWidgetsSequence());
    }

    private IEnumerator ShowWidgetsSequence()
    {
        for (int i = 0; i < widgets.Count; i++)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            widgets[i].ShowSequence();
        }
    }

    private IEnumerator HideWidgetsSequence()
    {
        for (int i = 0; i < widgets.Count; i++)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            widgets[i].HideSequence();
        }
    }
}
