using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetchupBottle : ItemDispenser
{
    [SerializeField] private float projectileSpeed;
    
    private void Awake()
    {
        PoolableFactory.InitPool(itemPool, itemToSpawn, itemPool.PoolMaxSize);
    }

    public override void SpawnItem()
    {
        bool spawnSuccess = false;
        IPoolable spawnedProjectile = PoolableFactory.TryRetrieveObject(itemPool, out spawnSuccess);

        if (spawnSuccess)
        {
            spawnedProjectile.SetPoolablePositionAndRotation(itemSpawnPosition);
            spawnedProjectile.GameObject.GetComponent<Rigidbody>().velocity = projectileSpeed * itemSpawnPosition.forward;
        }
        else
        {
        }
    }
}
