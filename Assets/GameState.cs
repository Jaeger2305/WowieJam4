using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public UnityEvent<bool> gameLost;
    public UnityEvent<bool> gameWon;
    [SerializeField] private bool runOnStart = false; // For the end scene, we want to know the state as soon as the scene has loaded. Another hack.

    void Start()
    {
        if (runOnStart)
        {
            bool isGameWon = PlayerPrefs.GetInt("IsGameWon") != 0;
            gameLost.Invoke(!isGameWon);
            gameWon.Invoke(isGameWon);
        }
    }

    public void LoseGame()
    {
        PlayerPrefs.SetInt("IsGameWon", 0);
        gameLost.Invoke(true);
    }
    public void WinGame()
    {
        PlayerPrefs.SetInt("IsGameWon", 1);
        gameWon.Invoke(true);
    }
}
