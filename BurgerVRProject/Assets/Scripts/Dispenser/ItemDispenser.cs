using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDispenser : MonoBehaviour
{
    [SerializeField] private ObjectPool itemPool;
    [SerializeField] private SpawnableObject itemToSpawn;
    [SerializeField] private Transform spawnPosition;

    private void Awake()
    {
        PoolableFactory.InitPool(itemPool, itemToSpawn, itemPool.PoolMaxSize);
    }

    public void SpawnItem()
    {
        PoolableFactory.TryCreateObject(itemPool, spawnPosition);
    }
}
