using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDispenser : MonoBehaviour
{
    public Transform ItemSpawnPoint => itemSpawnPosition;

    [SerializeField] protected ObjectPool itemPool;
    [SerializeField] protected SpawnableObject itemToSpawn;
    [SerializeField] protected Transform itemSpawnPosition;
    private AudioSource cachedAudioSource;

    private void Awake()
    {
        PoolableFactory.InitPool(itemPool, itemToSpawn, itemPool.PoolMaxSize);
        cachedAudioSource = GetComponent<AudioSource>();
    }

    public virtual void SpawnItem()
    {
        bool spawnSuccess = false;
        
        IPoolable item = PoolableFactory.TryRetrieveObject(itemPool, out spawnSuccess);
        
        if (spawnSuccess)
        {
            item.SetPoolablePositionAndRotation(itemSpawnPosition);
        }
    }
}
