using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoolableFactory
{
    public static void InitPool(ObjectPool pool, IPoolable poolableToCreate, int poolSize)
    {
        pool.InitPool(poolableToCreate, poolSize);
    }

    public static IPoolable TryCreateObject(ObjectPool pool, Transform spawnPoint = null)
    {
        IPoolable product = pool.TryGetPooledObject(spawnPoint);
        product.OnPoolableObjectEnable();
        return product;
    }
}
