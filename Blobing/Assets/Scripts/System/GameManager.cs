using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private SaveScript saveScript;

    public LevelData currentLevel;
    [SerializeField] private LevelList levelList;
    public int currentGold;

    public delegate void PowerUpEvent(float amount);
    public PowerUpEvent TrailUp;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(gameObject); }

        saveScript = new SaveScript();
        //saveScript.DeleteSave();
        int levelID = saveScript.GetSavedLevel();
        //currentLevel = levelList.levels[levelID-1];
        currentGold = saveScript.GetSavedGold();

        Screen.orientation = ScreenOrientation.Portrait;
    }


    public void StartGame()
    {
     
        StartCoroutine(LoadLevel());
    }

    public IEnumerator EndGame(bool playerWins)
    {

        yield return null;

        if (playerWins)
        {
             IncrementLevel();
        }
    }

    public void IncrementLevel()
    {
        int newLevelID = Mathf.Min(currentLevel.id + 1, levelList.levels.Length);
        saveScript.Save(newLevelID, currentGold);
        currentLevel = levelList.levels[newLevelID-1];
    }

    IEnumerator LoadLevel()
    {
        LevelGenerator.instance.ClearLevel();
        yield return null;
        LevelGenerator.instance.GenerateLevel(currentLevel);
    }

}
