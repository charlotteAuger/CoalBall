using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public static Target Instance;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(gameObject); }

    }

    private List<PointGiver> pointGivers = new List<PointGiver>();
    private int playerScore = 0;
    private int aiScore = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            pointGivers.Add(other.gameObject.GetComponent<PointGiver>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            pointGivers.Remove(other.gameObject.GetComponent<PointGiver>());
        }
    }

    public void UpdateScore()
    {
        List<PointGiver> aiPoints = pointGivers.FindAll(p => p.isOwnedByPlayer == false);
        List<PointGiver> playerPoints = pointGivers.FindAll(p => p.isOwnedByPlayer == true);

        int pScore = 0;
        int aScore = 0;

        foreach (PointGiver p in playerPoints)
        {
            pScore += p.GetPointValue();
        }

        foreach (PointGiver p in aiPoints)
        {
            aScore += p.GetPointValue();
        }

        playerScore = pScore;
        aiScore = aScore;

        UIManager.Instance.UpdateScore(playerScore, aiScore);

    }

    public bool CheckIfPlayerWins()
    { 
        return playerScore > aiScore;
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

}
