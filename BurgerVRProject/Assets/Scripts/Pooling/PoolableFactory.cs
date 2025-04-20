using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoolableFactory
{
    public static void InitPool(ObjectPool pool, IPoolable poolableToCreate, int poolSize)
    {
        pool.InitPool(poolableToCreate, poolSize);
    }

    public static IPoolable TryCreateObject(ObjectPool pool)
    {
        IPoolable product = pool.TryGetPooledObject();
        product.OnPoolableObjectEnable();
        return product;
    }
}
