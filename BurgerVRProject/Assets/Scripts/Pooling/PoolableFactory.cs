using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoolableFactory
{
    public static void InitPool(ObjectPool pool, IPoolable poolableToCreate, int poolSize)
    {
        pool.InitPool(poolableToCreate, poolSize);
    }

    public static IPoolable TryRetrieveObject(ObjectPool pool, out bool success)
    {
        IPoolable product = pool.TryGetPooledObject(out success);
        if (success)
        {
            product.OnPoolableObjectEnable();
        }
        return product;
    }
}
