using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    [SerializeField]
    GameObject startScreen;

    [SerializeField]
    GameObject gameLoop;

    [SerializeField]
    GameObject restartScreen;

    void Awake()
    {
        startScreen.SetActive(false);
        gameLoop.SetActive(false);
        restartScreen.SetActive(false);
    }

    void Start()
    {
        startScreen.SetActive(true);
    }

    public void StartToGame() {
        startScreen.SetActive(false);
        gameLoop.SetActive(true);
    }

    public void GameToRestart() {
        gameLoop.SetActive(false);
        restartScreen.SetActive(true);
    }

    public void RestartToStart() {
        restartScreen.SetActive(false);
        startScreen.SetActive(true);
    }

}
