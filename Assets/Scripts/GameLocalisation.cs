using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLocalisation : MonoBehaviour
{
    #region Singleton
    public static GameLocalisation Instance { get { return instance; } }
    private static GameLocalisation instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    #endregion Singleton

    [SerializeField] List<Sprite> sprites = default;
    [SerializeField] List<Color> colors = default;
    [SerializeField] Material defaultGameplayParticleMaterial = default;

    public Material DefaultGameplayParticleMaterial { get { return defaultGameplayParticleMaterial; }}

    public Sprite GetGameplayActionSprite(GameplayAction action, bool buttonPressedDownVersion = false)
    {
        Sprite sprite = null;
        switch (action)
        {
            case GameplayAction.DebugKey:
                break;
            case GameplayAction.LeftMain:
                sprite = sprites[0];
                break;
            case GameplayAction.RightMain:
                sprite = sprites[1];
                break;
            case GameplayAction.LeftSpecial:
                sprite = sprites[2];
                break;
            case GameplayAction.RightSpecial:
                sprite = sprites[3];
                break;
            case GameplayAction.ArenaPause:
                sprite = sprites[4];
                break;
            case GameplayAction.ArenaNext:
                sprite = sprites[5];
                break;
            case GameplayAction.ArenaPrevious:
                sprite = sprites[6];
                break;
            case GameplayAction.ArenaShake:
                if (buttonPressedDownVersion)
                    sprite = sprites[8];
                else
                    sprite = sprites[7];
                break;
            case GameplayAction.DebugSpawnBall:
                sprite = sprites[9];
                break;
            default:
                break;
        }
        return sprite;
    }

    public Color GetGameplayActionColor(GameplayAction action)
    {
        Color color = Color.white;

        switch (action)
        {
            case GameplayAction.DebugKey:
                break;
            case GameplayAction.LeftMain:
                color = colors[0];
                break;
            case GameplayAction.RightMain:
                color = colors[1];
                break;
            case GameplayAction.LeftSpecial:
                color = colors[2];
                break;
            case GameplayAction.RightSpecial:
                color = colors[3];
                break;
            case GameplayAction.ArenaPause:
                color = colors[4];
                break;
            case GameplayAction.ArenaNext:
                color = colors[5];
                break;
            case GameplayAction.ArenaPrevious:
                color = colors[6];
                break;
            case GameplayAction.ArenaShake:
                color = colors[7];
                break;
            case GameplayAction.DebugSpawnBall:
                color = colors[8];
                break;
            default:
                break;
        }

        return color;
    }
}
