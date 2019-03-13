using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public Poolable[] pool;

    private void Awake()
    {
        if (instance == null) instance = this;

        else Destroy(this);
    }

    public Poolable CreateBall(Vector3 position, bool ownedByPlayer, int typeId)
    {
        //Check pool for available bullet
        Poolable result = null;

        

        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].isAvailable)
            {
                result = pool[i];
                break;
            }
        }

        result.Spawn();
        result.transform.position = position;

        return result;
    }

    public void DestroyObject(Poolable pToDestroy)
    {
        pToDestroy.Disable(); 
    }
}
