using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject DeathScreen;
    [SerializeField] private TMP_Text ScoreCounter;
    [SerializeField] private TMP_Text highScoreCounter;
    [SerializeField] private TMP_Text framerateText;
    [SerializeField] private TMP_Text highScoreText;

    private void Start()
    {
        highScoreCounter.text = PlayerPrefs.GetInt("Highscore").ToString("00");
    }

    private void OnEnable()
    {
        Clownball.OnPlayerDeath += SetDeathScreenActive;
        ScoreManager.OnScoreUpdated += UpdateCurrentScoreText;
        ScoreManager.OnHighScoreAchived += SetHighScoreMessageActive;
    }
    private void OnDisable()
    {
        Clownball.OnPlayerDeath -= SetDeathScreenActive;
        ScoreManager.OnScoreUpdated -= UpdateCurrentScoreText;
        ScoreManager.OnHighScoreAchived += SetHighScoreMessageActive;
    }

    void SetDeathScreenActive()
    {
        DeathScreen.SetActive(true);
    }

    void SetHighScoreMessageActive()
    {
        highScoreText.gameObject.SetActive(true);
    }

    void UpdateCurrentScoreText(int currentScore)
    {
        ScoreCounter.text = currentScore.ToString();
    }

    private void Update()
    {
        framerateText.text = (1.0f / Time.smoothDeltaTime).ToString();
    }
}
