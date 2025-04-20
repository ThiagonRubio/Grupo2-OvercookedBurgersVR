using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TestCubeSpawner : MonoBehaviour
{
    // Esto es un contraldor para spwanear cosas para probar los pools, por si no se dieron cuenta.
    // Cada medio segundo dejamos caer una cajita para probar


    [SerializeField] private ObjectPool testCubePool;
    [SerializeField] private _TestCubePoolableObject testCubePrefabToSpawn;

    float testTimeCounter = 0.0f;
    float testSpawnTime = 0.5f;

    //-----------------------METHODS----------------------------------

    private void Awake()
    {
        PoolableFactory.InitPool(testCubePool, testCubePrefabToSpawn, testCubePool.PoolMaxSize);
    }

    // No se les ocurra usar el update
    private void Update()
    {
        testTimeCounter += Time.deltaTime;

        if (testTimeCounter >= testSpawnTime)
        {
            PoolableFactory.TryCreateObject(testCubePool);
            testTimeCounter = 0.0f;
        }
    }
}
