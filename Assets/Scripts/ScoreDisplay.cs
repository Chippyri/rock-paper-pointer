using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    int playerScore;
    int enemyScore;

    [SerializeField]
    Image playerScoreImage;

    [SerializeField]
    Image enemyScoreImage;

    void OnEnable()
    {
        playerScore = 0;
        enemyScore = 0;
        updateEnemyScoreSprite();
        updatePlayerScoreSprite();
    }

    public void incrementEnemyScoreAndUpdateSprite() {
        enemyScore = (enemyScore + 1) % 10; // TODO: Change
        updateEnemyScoreSprite();
    }

    public void incrementPlayerScoreAndUpdateSprite()
    {
        playerScore = (playerScore + 1) % 10; // TODO: Change
        updatePlayerScoreSprite();
    }

    void updateEnemyScoreSprite() {
        enemyScoreImage.sprite = NumberImageFactory.getNumberSprite(enemyScore, false);
    }

    void updatePlayerScoreSprite()
    {
        playerScoreImage.sprite = NumberImageFactory.getNumberSprite(playerScore, true);
    }
}
