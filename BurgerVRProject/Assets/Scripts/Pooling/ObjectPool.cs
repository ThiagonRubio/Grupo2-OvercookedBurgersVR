using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Queue<IPoolable> poolQueue = new Queue<IPoolable>();
    private IPoolable objectToPool;
    private int poolSize = 10;

    public ObjectPool(IPoolable objectToPool, int poolMaxSize = 10)
    {
        if (poolQueue != null)
        {
            this.objectToPool = objectToPool;
            this.poolSize = poolMaxSize;
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("Object is not a poolable object.");
#endif
        }
    }

    //-----------------------METHODS----------------------------------

    public IPoolable TryGetPooledObject(out bool success)
    {
        IPoolable pooledObject = null;

        if (poolQueue.Count < poolSize)
        {
            success = false;
        }
        else
        {
            pooledObject = ReuseObject(out success);
        }

        if (success)
        {
            poolQueue.Enqueue(pooledObject);
        }

        return pooledObject;
    }

    private IPoolable ReuseObject(out bool success)
    {
        // Solo vamos a intentar reusar cuantos objetos haya en la pool
        int maxAttempts = poolQueue.Count;
        int attemptCount = 0;

        while (poolQueue.Count > 0 && attemptCount < maxAttempts)
        {
            attemptCount++;
            IPoolable pooledObject = poolQueue.Dequeue();

            if (pooledObject.IsAvailable)
            {
                success = true;
                return pooledObject;
            }
            else
            {
                poolQueue.Enqueue(pooledObject);
            }
        }

        // Si sale del loop no hay nada disponible para agarrar del pool en este momento - intente denuevo mas tarde
        success = false;
        return null;

    }

    // --- AUX HELPER METHODS ----

    public bool IsPoolFull()
    {
        return poolQueue.Count >= poolSize;
    }
    public int GetQueueCount()
    {
        return poolQueue.Count;
    }
    public void EnqueuePoolable(IPoolable toEnqueue)
    {
        poolQueue.Enqueue(toEnqueue);
    }
}
