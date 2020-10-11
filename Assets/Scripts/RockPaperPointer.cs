using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RockPaperPointer : MonoBehaviour
{
    private const int SCORE_TO_WIN = 3;
    private enum WinnerResult {
        ENEMY = -1, TIE, PLAYER 
    };

    int playerScore;
    int enemyScore;

    [SerializeField]
    EnemyHand enemy;

    [SerializeField]
    PlayerHand player;

    [SerializeField]
    Image playerWin;

    [SerializeField]
    Image playerLose;

    [SerializeField]
    Image restarting;

    [SerializeField]
    GameObject gameLoopDisplays;

    [SerializeField]
    SoundPlayer soundPlayer;

    [SerializeField]
    public UnityEngine.Events.UnityEvent OnPlayerScored;

    [SerializeField]
    public UnityEngine.Events.UnityEvent OnEnemyScored;

    [SerializeField]
    public UnityEngine.Events.UnityEvent OnTie;

    [SerializeField]
    public UnityEngine.Events.UnityEvent OnGameEnd;

    [SerializeField]
    public UnityEngine.Events.UnityEvent OnGameRestart;

    // Start is called before the first frame update
    void OnEnable()
    {
        Init();
    }

    void Init() {
        enemyScore = 0;
        playerScore = 0;

        playerWin.enabled = false;
        playerLose.enabled = false;
        restarting.enabled = false;
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    public void OnHandShown() {
        HandleResult(EvaluateResult(player.GetState(), enemy.GetState()));
    }

    private void HandleResult(WinnerResult winnerResult)
    {
        switch (winnerResult){
            case WinnerResult.PLAYER:
                HandlePlayerWin();
                break;
            case WinnerResult.TIE:
                HandleTie();
                break;
            case WinnerResult.ENEMY:
                HandleEnemyWin();
                break;
        }
        CheckForGameEndAndHandleIt();
    }

    private WinnerResult EvaluateResult(HandState playerHandState, HandState enemyHandState)
    {
        // The player loses if no hand is seen
        if (playerHandState == HandState.UNKNOWN) {
            return WinnerResult.ENEMY;
        }

        if (playerHandState.Equals(enemyHandState))
        {
            return WinnerResult.TIE;
        }
        
        // Yes, this needs refactoring... :)
        if (playerHandState == HandState.PAPER)
        {
            switch (enemyHandState)
            {
                case HandState.POINTER:
                    return WinnerResult.ENEMY;
                case HandState.ROCK:
                    return WinnerResult.PLAYER;
            }
        }
        
        if (playerHandState == HandState.POINTER) {
            switch (enemyHandState)
            {
                case HandState.PAPER:
                    return WinnerResult.PLAYER;
                case HandState.ROCK:
                    return WinnerResult.ENEMY;
            }
        }
        
        if (playerHandState == HandState.ROCK)
        {
            switch (enemyHandState)
            {
                case HandState.PAPER:
                    return WinnerResult.ENEMY;
                case HandState.POINTER:
                    return WinnerResult.PLAYER;
            }
        }

        return 0;
    }

    void HandlePlayerWin()
    {
        soundPlayer.PlayRoundWinSound();
        playerScore++;
        if (OnPlayerScored != null) {
            OnPlayerScored.Invoke();
        }
    }

    void HandleEnemyWin() {
        soundPlayer.PlayRoundLoseSound();
        enemyScore++;
        if (OnEnemyScored != null)
        {
            OnEnemyScored.Invoke();
        }
    }

    void HandleTie()
    {
        soundPlayer.PlayRoundTieSound();
        if (OnTie != null)
        {
            OnTie.Invoke();
        }
    }

    void CheckForGameEndAndHandleIt() {
        if (playerScore >= SCORE_TO_WIN)
        {
            // Show player win screen
            soundPlayer.PlayGameWinSound();
            playerWin.enabled = true;
        } 
        else if (enemyScore >= SCORE_TO_WIN)
        {
            // Show player lose screen
            soundPlayer.PlayGameLoseSound();
            playerLose.enabled = true;
        }
        else {
            // Neither won yet
            return;
        }

        // Show restarting and start countdown to restart in 5 seconds...
        restarting.enabled = true;
        StartCoroutine(nameof(RestartGame));
    }

    IEnumerator RestartGame() {
        if (OnGameEnd != null) {
            OnGameEnd.Invoke();
        }
        yield return new WaitForSeconds(5);
        if (OnGameRestart != null)
        {
            Init();
            OnGameRestart.Invoke();
        }
    }
}
