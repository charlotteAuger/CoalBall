using System.Collections;
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

    public IEnumerator TurnAnnounce(bool isPlayerTurn)
    {
        turnText.gameObject.SetActive(true);
        if (isPlayerTurn)
        {
            turnText.text = "Your turn!";
        }
        else
        {
            turnText.text = "Enemy's turn!";
        }

        yield return new WaitForSeconds(timeOnScreen);

        turnText.gameObject.SetActive(false);
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
