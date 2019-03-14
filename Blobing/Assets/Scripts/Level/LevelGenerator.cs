using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance = null;
    [SerializeField] private Transform[] spawnPoints;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    public void GenerateLevel(LevelData data)
    {
        
        
    }

    public void ClearLevel()
    {
        print("Clear !");
        
    }
}
