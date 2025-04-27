using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDispenser : MonoBehaviour
{
    public Transform ItemSpawnPoint => itemSpawnPosition;

    [SerializeField] private ObjectPool itemPool;
    [SerializeField] private SpawnableObject itemToSpawn;
    [SerializeField] private Transform itemSpawnPosition;

    private void Awake()
    {
        PoolableFactory.InitPool(itemPool, itemToSpawn, itemPool.PoolMaxSize);
    }

    public void SpawnItem()
    {
        bool spawnSuccess = false;
        PoolableFactory.TryRetrieveObject(itemPool, out spawnSuccess);

        if (spawnSuccess)
        {
        }
        else
        {
            Debug.Log("Usá los items spawneados, vos podés."); //Acá iría un feedback de que ya no se puede
        }
    }
}
