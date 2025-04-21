using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetchupBottle : MonoBehaviour
{
    [SerializeField] private ObjectPool projectilePool;
    [SerializeField] private SpawnableObject ketchupProjectile;

    [SerializeField] private float projectileSpeed;
    [SerializeField] private Transform bottlePoint;

    private void Awake()
    {
        PoolableFactory.InitPool(projectilePool, ketchupProjectile, projectilePool.PoolMaxSize);
    }

    public void Use()
    {
        var spawnedProjectile = PoolableFactory.TryCreateObject(projectilePool, bottlePoint);
        spawnedProjectile.GameObject.GetComponent<Rigidbody>().velocity = projectileSpeed * bottlePoint.forward;
    }
}
