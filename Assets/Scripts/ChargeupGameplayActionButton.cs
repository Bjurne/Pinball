using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeupGameplayActionButton : GameplayActionButton
{
    [SerializeField] private float chargeupTime = 1f;
    [SerializeField] private Image chargeupBarImage;
    [SerializeField] private float buttonShakeForce;
    private Coroutine chargeupSequence;
    private float timer = 0f;
    private Vector3 originalPosition;
    private bool activeSpriteIsButtonPressedSprite = false;

    public new void OnButtonPressed()
    {
        if (chargeupSequence == null)
            chargeupSequence = StartCoroutine(ChargeupSequence());
    }

    public new void OnButtonReleased()
    {
        if (chargeupSequence != null)
        {
            base.OnButtonReleased();
            StopCoroutine(chargeupSequence);
            Reset();
        }
        if (activeSpriteIsButtonPressedSprite)
        {
            symbolImage.sprite = GameLocalisation.Instance.GetGameplayActionSprite(Action, false);
            activeSpriteIsButtonPressedSprite = false;
        }
    }

    private IEnumerator ChargeupSequence()
    {
        originalPosition = transform.localPosition;
        chargeupBarImage.transform.SetParent(transform.parent);

        while (timer < chargeupTime)
        {
            timer += Time.deltaTime;
            chargeupBarImage.fillAmount = Mathf.Lerp(0f, 1f, timer / chargeupTime);
            var randomPositionOffset = new Vector3(UnityEngine.Random.Range(-buttonShakeForce, buttonShakeForce), UnityEngine.Random.Range(-buttonShakeForce, buttonShakeForce), UnityEngine.Random.Range(-buttonShakeForce, buttonShakeForce));
            transform.localPosition = originalPosition + randomPositionOffset;
            yield return new WaitForEndOfFrame();
        }
        Reset();
        symbolImage.sprite = GameLocalisation.Instance.GetGameplayActionSprite(Action, true);
        activeSpriteIsButtonPressedSprite = true;
        base.OnButtonPressed();
    }

    private void Reset()
    {
        chargeupBarImage.transform.SetParent(transform);
        chargeupBarImage.transform.localPosition = Vector3.zero;
        transform.localPosition = originalPosition;
        timer = 0f;
        chargeupBarImage.fillAmount = 0f;
        chargeupSequence = null;
    }
}
