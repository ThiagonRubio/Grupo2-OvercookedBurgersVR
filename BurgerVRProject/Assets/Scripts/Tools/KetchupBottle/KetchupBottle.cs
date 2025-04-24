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

    // README: SUPER IMPORTANTE
    // POR QUE ESTO NO HEREDA DE ITEMDISPENSER SI SON LITERALMENTE LO MISMO SALVO 1 LINEA DE CODIGO? NO ME HAGAN VIOLENTAR
    // <<<<----------------------------------------------

    private void Awake()
    {
        PoolableFactory.InitPool(projectilePool, ketchupProjectile, projectilePool.PoolMaxSize);
    }

    public void Use()
    {
        bool spawnSuccess = false;
        IPoolable spawnedProjectile = PoolableFactory.TryRetrieveObject(projectilePool, out spawnSuccess);

        if (spawnSuccess)
        {
            spawnedProjectile.GameObject.GetComponent<Rigidbody>().velocity = projectileSpeed * bottlePoint.forward;
        }
        else
        {
        }
    }
}
