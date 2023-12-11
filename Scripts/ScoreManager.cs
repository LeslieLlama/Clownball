using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> OnScoreUpdated;
    public static event Action OnHighScoreAchived;

    public int currentScore;
    public int highScore;

    private void OnEnable()
    {
        Coin.OnCoinPickup += CoinCollected;
        Clownball.OnPlayerDeath += CheckForHighScore;
    }
    private void OnDisable()
    {
        Coin.OnCoinPickup -= CoinCollected;
        Clownball.OnPlayerDeath -= CheckForHighScore;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("Highscore");
    }

    void CoinCollected(GameObject coin)
    {
        currentScore += coin.GetComponent<Coin>().coinToAward;
        OnScoreUpdated(currentScore);
        
    }

    void CheckForHighScore()
    {
        if (currentScore >= highScore)
        {
            highScore = currentScore;
            OnHighScoreAchived();
        }
        PlayerPrefs.SetInt("Highscore", highScore);
    }
}
