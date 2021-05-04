using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControlledWidget : MonoBehaviour, IGameplayPausable, IInputParticlesWidgetController
{
    //[SerializeField, Range(0, 359)] float startRotation;
    //private float minRotation;
    //[SerializeField, Range(-300, 300)] float maxRotation;
    [SerializeField] internal Transform widgetPivot;
    [SerializeField] internal Rigidbody rigidBody;
    //[SerializeField] float punchPower;
    [SerializeField] internal GameplayAction key;

    internal bool keyIsBeingPressed = false;
    internal bool initialized { get; private set; }
    private InputParticlesWidget inputParticlesWidget;

    private bool isPaused;
    public bool IsPaused => isPaused;

    private void Start()
    {
        Setup();
    }

    internal void Setup()
    {
        //rigidBody.centerOfMass = widgetPivot.transform.localPosition;
        //rigidBody.maxAngularVelocity = 14f;
        ////if (startRotation >= maxRotation)
        ////    punchPower *= -1;
        //if (maxRotation < 0f)
        //    punchPower *= -1;

        ArenaControlBoard.Instance.OnBroadcastButtonPressed += OnButtonPressed;
        ArenaControlBoard.Instance.OnBroadcastButtonReleased += OnButtonReleased;
        initialized = true;

        inputParticlesWidget = GetComponentInChildren<InputParticlesWidget>();
        UpdateInputParticlesWidget();
    }

    internal virtual void OnEnable()
    {
        if (initialized)
        {
            ArenaControlBoard.Instance.OnBroadcastButtonPressed += OnButtonPressed;
            ArenaControlBoard.Instance.OnBroadcastButtonReleased += OnButtonReleased;
        }
    }

    private void OnDisable()
    {
        ArenaControlBoard.Instance.OnBroadcastButtonPressed -= OnButtonPressed;
        ArenaControlBoard.Instance.OnBroadcastButtonReleased -= OnButtonReleased;
    }

    internal virtual void OnButtonPressed(GameplayAction key)
    {
        if (key == this.key)
        {
            keyIsBeingPressed = true;
        }
    }

    internal virtual void OnButtonReleased(GameplayAction key)
    {
        if (key == this.key)
        {
            keyIsBeingPressed = false;
        }
    }

    public void UpdateInputParticlesWidget()
    {
        if (inputParticlesWidget != null)
            inputParticlesWidget.SetKey(key);
    }

    public void SetPaused(bool gameplayIsBeingPaused)
    {
        isPaused = gameplayIsBeingPaused;
        if (inputParticlesWidget != null)
            inputParticlesWidget.gameObject.SetActive(!IsPaused); // Fix proper PauseInputParticles()
        rigidBody.freezeRotation = gameplayIsBeingPaused;
    }
}

