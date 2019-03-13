using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance = null;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] closestSpawnPoints;
    private GameObject[] circles;
    public float checkTiming;
    public bool on;
    public GameObject circlePrefab;

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

    private Vector3 GetRandomPositionFromSpawnpoint(Transform spawnPoint)
    {
        Vector2 randomInRange = Random.insideUnitCircle;
        Vector3 spawnPosition = spawnPoint.position + new Vector3(randomInRange.x, 0, randomInRange.y) * 5f;
        return spawnPosition;
    }

    private void CheckList(ref List<Transform> list)
    {
        if (list.Count > 0) return;

        else list = spawnPoints.ToList<Transform>();
    }

    public void AddElement(Poolable element)
    {
       
    }

    public void RemoveElement(Poolable element)
    {
       
    }

    public void ClearLevel()
    {
        print("Clear !");
        
    }
}
