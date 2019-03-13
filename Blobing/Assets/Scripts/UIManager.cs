﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(gameObject); }

    }

    [Header("Score and Level")]
    [SerializeField] private GameObject gameUICanvas;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI aiScoreText;
    [SerializeField] private SpriteRenderer[] playerBallsLeft;
    [SerializeField] private SpriteRenderer[] aiBallsLeft;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private TextMeshProUGUI currentGold;

    [Header("Transition")]
    [SerializeField] private GameObject transitionScreenCanvas;
    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Turns")]
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private float timeOnScreen;

    public void InitializeInGameUI(int _currentLevel)
    {
        UpdateScore(0, 0);
        gameUICanvas.SetActive(true);
    }

    public void HideGameUI()
    {
        gameUICanvas.SetActive(false);
    }

    public void UpdateScore(float pScore, float aScore)
    {
        playerScoreText.text = pScore.ToString();
        aiScoreText.text = aScore.ToString();
    }

    public void UpdateBallsLeft(int pbLeft, int abLeft)
    {
        for (int i = 0; i < playerBallsLeft.Length; i++)
        {
            playerBallsLeft[i].enabled = i < pbLeft;
            aiBallsLeft[i].enabled = i < abLeft;
        }
    }

    public IEnumerator TurnAnnounce(bool isPlayerTurn)
    {
        turnText.gameObject.SetActive(true);
        turnText.text = isPlayerTurn ? "Your turn!" : "Enemy's turn!";

        yield return new WaitForSeconds(timeOnScreen);

        turnText.gameObject.SetActive(false);
        TurnManager.Instance.BeginTurn();
    }

    public void SetMainMenuVisibility(bool state)
    {
        mainMenuCanvas.SetActive(state);
    }

    public void SetTransitionUI(bool victory, int points)
    {
        if (victory)
        {
            victoryText.text = "You won!";
            coinText.text = "+" + points*2;
        }
        else
        {
           victoryText.text = "You lost...";
           coinText.text = "+" + points;
        }

        transitionScreenCanvas.SetActive(true);
    }

    public void HideTransitionUI()
    {
        transitionScreenCanvas.SetActive(false);
    }
}
