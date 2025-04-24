using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TestCubeSpawner : MonoBehaviour
{
    // Esto es un contraldor para spwanear cosas para probar los pools, por si no se dieron cuenta.
    // Cada segundo dejamos caer una cajita para probar


    [SerializeField] private ObjectPool testCubePool;
    [SerializeField] private _TestCubePoolableObject testCubePrefabToSpawn;

    float testTimeCounter = 0.0f;
    float testSpawnTime = 1.0f;
    float testDisableCounter = 0.0f;
    float testDisableTime = 4.5f;

    //-----------------------METHODS----------------------------------

    private void Awake()
    {
        PoolableFactory.InitPool(testCubePool, testCubePrefabToSpawn, testCubePool.PoolMaxSize);
    }

    // No se les ocurra usar el update
    private void Update()
    {
        testTimeCounter += Time.deltaTime;
        testDisableCounter += Time.deltaTime;

        if (testTimeCounter >= testSpawnTime)
        {
            bool spawnSuccess;
            IPoolable product = PoolableFactory.TryRetrieveObject(testCubePool, out spawnSuccess); 

            if (spawnSuccess)
            {
            #if UNITY_EDITOR
                            Debug.Log("Pude reusar una cajita :)");
            #endif
            }
            else
            {
                // Aca va a ser nulo - a vos te hablo facundo
            #if UNITY_EDITOR
                            Debug.Log("No habia cajitas disponibles para reusar :(");
            #endif
            }
            testTimeCounter = 0.0f;
        }

        // Solo TEST - despues deciden como cuando se puede poolear o no como quieran. PS no vuelvan a tocar el ObjectPool
        if (testDisableCounter >= testDisableTime)
        {
            foreach (IPoolable poolable in testCubePool.ObjectsInPool)
            {
                poolable.CanBePooled = !poolable.CanBePooled;
            }
            testDisableCounter = 0.0f;
        }
    }
}
