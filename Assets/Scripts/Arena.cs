using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    #region Singleton
    public static Arena Instance;

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

    [SerializeField] UIPresenterView CheatGetMainMenu = default;
    [SerializeField] ArenaPropsSet[] arenaPropsSets;
    [SerializeField] ArenaPropsSet currentlyActiveArena;
    [SerializeField] private int numberOfBallsInStockpile = default;
    private ArenaPropsSet[] arenas;
    private bool initialized = false;
    private bool refillBallsFromStockpile = true;
    private bool gameplayIsPaused;

    private void Start()
    {
        arenas = new ArenaPropsSet[arenaPropsSets.Length];
        for (int i = 0; i < arenaPropsSets.Length; i++)
        {
            arenas[i] = Instantiate<ArenaPropsSet>(arenaPropsSets[i], transform);
            arenas[i].SetActive(false);
        }

        if (currentlyActiveArena != null)
        {
            var newGameObject = Instantiate(currentlyActiveArena, transform);
            currentlyActiveArena = newGameObject.GetComponent<ArenaPropsSet>();
            //currentlyActiveArena = arenas[Array.IndexOf(arenaPropsSets, currentlyActiveArena)];
            foreach (ArenaHole hole in currentlyActiveArena.GetComponentsInChildren<ArenaHole>())
            {
                hole.HoleType = ArenaHoleType.EndlessDebugHole;
            }
        }
        else
        {
            currentlyActiveArena = arenas[0];
        }
        //var startingArenaIndex = Array.Find(arenaPropsSets, p => currentlyActiveArena) != null ? Array.IndexOf(arenaPropsSets, currentlyActiveArena) : 0;
        //currentlyActiveArena = arenas[startingArenaIndex];
        currentlyActiveArena.SetActive(true);
        currentlyActiveArena.SpawnBall();

        UIEvents.Instance.OnStockpileUpdated?.Invoke(numberOfBallsInStockpile);

        ArenaControlBoard.Instance.OnBroadcastButtonPressed += OnButtonPressed;
        initialized = true;
        //ArenaControlBoard.Instance.OnBroadcastButtonReleased += OnButtonReleased;
    }

    internal void PickupCollected(PickupType pickupType)
    {
        switch (pickupType)
        {
            case PickupType.DefaultGameBall:
                numberOfBallsInStockpile += 1;
                UIEvents.Instance.OnStockpileUpdated?.Invoke(numberOfBallsInStockpile);
                break;

            default:
                break;
        }
    }

    internal void BallEnteredHole(ArenaHoleType goalType)
    {
        switch (goalType)
        {
            case ArenaHoleType.BackHole:
                if (currentlyActiveArena == arenas[0] && numberOfBallsInStockpile < 1)
                {
                    Debug.Log($"YOU LOSE");
                    return;
                }

                if (refillBallsFromStockpile && numberOfBallsInStockpile > 0)
                {
                    numberOfBallsInStockpile -= 1;
                    UIEvents.Instance.OnStockpileUpdated?.Invoke(numberOfBallsInStockpile);
                }
                else
                {
                    PreviousArena();
                }
                break;

            case ArenaHoleType.EndlessDebugHole:
                break;

            case ArenaHoleType.GoalHole:
                NextArena();
                break;

            default:
                break;
        }

        currentlyActiveArena.SpawnBall();
    }

    public void NextArena()
    {
        currentlyActiveArena.SetActive(false);
        var nextArenaIndex = Array.IndexOf(arenas, currentlyActiveArena) + 1;
        if (nextArenaIndex > arenas.Length - 1)
            nextArenaIndex = 0;
        currentlyActiveArena = arenas[nextArenaIndex];
        currentlyActiveArena.SetActive(true);
    }

    public void PreviousArena()
    {
        currentlyActiveArena.SetActive(false);
        var previousArenaIndex = Array.IndexOf(arenas, currentlyActiveArena) - 1;
        if (previousArenaIndex < 0)
            previousArenaIndex = 0;
        //previousArenaIndex = arenas.Length - 1;
        currentlyActiveArena = arenas[previousArenaIndex];
        currentlyActiveArena.SetActive(true);
    }

    private void ShakeArena()
    {
        var activeBalls = FindObjectsOfType<GameBall>();
        for (int i = 0; i < activeBalls.Length; i++)
        {
            var rb = activeBalls[i].GetComponent<Rigidbody>();
            var randomPower = UnityEngine.Random.Range(-0.5f, 0.5f);
            rb.AddRelativeForce(new Vector3(randomPower, randomPower, 0f), ForceMode.Impulse);
            //StartCoroutine(ShakeTransform(activeBalls[i].transform, false)); // Could work with math using the Arenas local directions
        }

        StartCoroutine(ShakeTransform(Camera.main.transform, true));

        StartCoroutine(JoltWidgets()); // Is this a good feature? "Unpresses" all currently pressed buttons
    }

    private IEnumerator JoltWidgets()
    {
        ArenaControlBoard.Instance.BroadcastButtonPressed(GameplayAction.LeftMain);
        ArenaControlBoard.Instance.BroadcastButtonPressed(GameplayAction.RightMain);

        yield return new WaitForSeconds(0.05f);

        ArenaControlBoard.Instance.BroadcastButtonReleased(GameplayAction.LeftMain);
        ArenaControlBoard.Instance.BroadcastButtonReleased(GameplayAction.RightMain);
    }

    private IEnumerator ShakeTransform(Transform transformToShake, bool returnToOriginalPosition)
    {
        var originalPos = transformToShake.localPosition;
        for (int i = 0; i < 10f; i++)
        {
            if (transformToShake == null)
                break;

            var randomX = UnityEngine.Random.Range(-0.5f, 0.5f);
            var randomY = UnityEngine.Random.Range(-0.5f, 0.5f);
            transformToShake.localPosition += new Vector3(randomX, randomY, 0f);

            yield return new WaitForEndOfFrame();
            if (returnToOriginalPosition)
                transformToShake.localPosition = originalPos;
        }

        if (returnToOriginalPosition)
            transformToShake.localPosition = originalPos;
    }

    internal Transform CurrentlyActivePropsSet()
    {
        return currentlyActiveArena.transform;
    }

    private void OnEnable()
    {
        if (initialized)
            ArenaControlBoard.Instance.OnBroadcastButtonPressed += OnButtonPressed;
    }

    private void OnDisable()
    {
        ArenaControlBoard.Instance.OnBroadcastButtonPressed -= OnButtonPressed;
    }

    private void OnButtonPressed(GameplayAction key)
    {
        switch (key)
        {
            case GameplayAction.ArenaPause:
                if (!gameplayIsPaused)
                {
                    if (CheatGetMainMenu != null)
                        CheatGetMainMenu.ShowSequence();
                    //Time.timeScale = 0f;
                    foreach (IGameplayPausable pausable in GetComponentsInChildren<IGameplayPausable>())
                    {
                        pausable.SetPaused(true);
                    }
                    gameplayIsPaused = true;
                }
                else
                {
                    if (CheatGetMainMenu != null)
                        CheatGetMainMenu.SetActive(false);
                    //Time.timeScale = 1f;
                    foreach (IGameplayPausable pausable in GetComponentsInChildren<IGameplayPausable>())
                    {
                        pausable.SetPaused(false);
                    }
                    gameplayIsPaused = false;
                }
                break;

            case GameplayAction.ArenaNext:
                NextArena();
                break;

            case GameplayAction.ArenaPrevious:
                PreviousArena();
                break;

            case GameplayAction.ArenaShake:
                ShakeArena();
                break;

            case GameplayAction.DebugKey:
                Debug.Log($"{key}");
                break;

            case GameplayAction.DebugSpawnBall: // Shouldnt use this input here, just for debugging
                currentlyActiveArena.SpawnBall();
                break;

            default:
                break;
        }
    }
}
