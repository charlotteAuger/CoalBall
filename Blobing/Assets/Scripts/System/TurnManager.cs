using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    public static TurnManager Instance;

    private bool isPlayerTurn;
    [SerializeField] private int nbrOfTurnPerPlayer;
    [SerializeField] private int nbrOfTurnFinished;
    [SerializeField] private AIController ai;
    [SerializeField] private SlingshotData data;


    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(gameObject); }

    }

    public void ChooseFirstPlayer()
    {

    }

    public IEnumerator PlayerTurn()
    {
        yield return null;
        EndTurn();
    }

    public IEnumerator AITurn()
    {
        yield return null;
        EndTurn();
    }

    public void EndTurn()
    {
        nbrOfTurnFinished++;
        isPlayerTurn = !isPlayerTurn;
        Target.Instance.UpdateScore();

        if (nbrOfTurnFinished >= nbrOfTurnPerPlayer * 2)
        {
            GameManager.Instance.EndGame(Target.Instance.CheckIfPlayerWins());
            return;
        }

        StartCoroutine(UIManager.Instance.TurnAnnounce(isPlayerTurn));

    }

    public void BeginTurn()
    {
        if (isPlayerTurn)
        {
            StartCoroutine(PlayerTurn());
        }
        else
        {
            StartCoroutine(AITurn());
        }
    }

}
