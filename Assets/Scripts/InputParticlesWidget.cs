using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputParticlesWidget : MonoBehaviour
{
    [SerializeField] ParticleSystem keyButtonPressedParticleSystem = default;
    [SerializeField] ParticleSystem keyButtonUnpressedParticleSystem = default;
    [SerializeField] GameplayAction manualOverrideKey = default;
    private GameplayAction key;
    private bool initialized;

    private void Start()
    {
        ArenaControlBoard.Instance.OnBroadcastButtonPressed += OnButtonPressed;
        ArenaControlBoard.Instance.OnBroadcastButtonReleased += OnButtonReleased;
        initialized = true;

        if (manualOverrideKey != GameplayAction.DebugKey)
            SetKey(manualOverrideKey);
        if (keyButtonUnpressedParticleSystem != null)
            keyButtonUnpressedParticleSystem.Play();
    }

    internal void SetKey(GameplayAction newKey)
    {
        key = newKey;
        UpdateParticleMaterial();
    }

    private void UpdateParticleMaterial()
    {
        var particleMaterial = new Material(GameLocalisation.Instance.DefaultGameplayParticleMaterial);
        particleMaterial.mainTexture = GameLocalisation.Instance.GetGameplayActionSprite(key).texture;
        particleMaterial.color = GameLocalisation.Instance.GetGameplayActionColor(key);
        //particleMaterial.EnableKeyword("_EMISSION");
        //particleMaterial.SetColor("_EmissionColor", GameLocalisation.Instance.GetGameplayActionColor(key));

        if (keyButtonPressedParticleSystem != null)
        {
            var psRendererPressed = keyButtonPressedParticleSystem.GetComponent<ParticleSystemRenderer>();
            psRendererPressed.material = particleMaterial;
        }
        if (keyButtonUnpressedParticleSystem != null)
        {
            var psRendererPressed = keyButtonUnpressedParticleSystem.GetComponent<ParticleSystemRenderer>();
            psRendererPressed.material = particleMaterial;
        }
    }

    private void OnEnable()
    {
        if (initialized)
        {
            ArenaControlBoard.Instance.OnBroadcastButtonPressed += OnButtonPressed;
            ArenaControlBoard.Instance.OnBroadcastButtonReleased += OnButtonReleased;

            if (keyButtonUnpressedParticleSystem != null)
                keyButtonUnpressedParticleSystem.Play();
        }
    }

    private void OnDisable()
    {
        ArenaControlBoard.Instance.OnBroadcastButtonPressed -= OnButtonPressed;
        ArenaControlBoard.Instance.OnBroadcastButtonReleased -= OnButtonReleased;
    }

    private void OnButtonPressed(GameplayAction key)
    {
        if (key == this.key)
        {
            if (keyButtonUnpressedParticleSystem)
            {
                keyButtonUnpressedParticleSystem.Stop();
                keyButtonUnpressedParticleSystem.Clear();
            }
            if (keyButtonPressedParticleSystem != null)
                keyButtonPressedParticleSystem.Play();
        }
    }

    private void OnButtonReleased(GameplayAction key)
    {
        if (key == this.key)
        {
            if (keyButtonPressedParticleSystem != null)
                keyButtonPressedParticleSystem.Stop();
            if (keyButtonUnpressedParticleSystem)
                keyButtonUnpressedParticleSystem.Play();
        }
    }
}

public interface IInputParticlesWidgetController
{
    void UpdateInputParticlesWidget();
}
