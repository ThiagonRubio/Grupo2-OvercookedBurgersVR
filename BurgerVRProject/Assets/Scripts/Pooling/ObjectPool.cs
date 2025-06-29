using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
{
    public int PoolMaxSize => poolSize;
    public Queue<IPoolable> ObjectsInPool => objectPool;

    private Queue<IPoolable> objectPool = new Queue<IPoolable>();
    private IPoolable objectToPool;
    [SerializeField] private int poolSize = 10;


    //-----------------------METHODS----------------------------------

    public void InitPool(IPoolable objectToPool, int poolMaxSize = 10)
    {
        if (objectPool != null)
        {
            this.objectToPool = objectToPool;
            this.poolSize = poolMaxSize;
        }
        else 
        {
#if UNITY_EDITOR
            Debug.LogWarning("Object is not a poolable object bro.");
#endif
        }

    }

    public IPoolable TryGetPooledObject(out bool success)
    {
        IPoolable pooledObject = null;

        if (objectPool.Count < poolSize)
        {
            pooledObject = NewObject();
            success = true;
        }
        else
        {
            pooledObject = ReuseObject(out success);
        }

        if (success)
        {
            objectPool.Enqueue(pooledObject);
        }

        return pooledObject;
    }

    private IPoolable NewObject()
    {
        GameObject newObject = Instantiate(objectToPool.GameObject, transform.position, transform.rotation);
        IPoolable pooledObject = newObject.GetComponent<IPoolable>();
        pooledObject.GameObject.name = transform.root.name + "_" + objectToPool.GameObject.name + "_" + objectPool.Count;
        pooledObject.GameObject.transform.SetParent(gameObject.transform);

        return pooledObject;
    }
    private IPoolable ReuseObject(out bool success)
    {
        // Solo vamos a intentar reusar cuantos objetos haya en la pool
        int maxAttempts = objectPool.Count;
        int attemptCount = 0;

        while (objectPool.Count > 0 && attemptCount < maxAttempts)
        {
            attemptCount++;
            IPoolable pooledObject = objectPool.Dequeue();

            if (pooledObject.IsAvailable)
            {
                success = true;
                return pooledObject;
            }
            else
            {
                objectPool.Enqueue(pooledObject);
            }
        }

        // Si sale del loop no hay nada disponible para agarrar del pool en este momento - intente denuevo mas tarde
        success = false;
        return null;

    }

    // --- AUX HELPER METHODS ---- (Por si las dudas porque los conozco)

    public int GetNumberOfActivePoolObjects()
    {
        int activeCount = 0;

        foreach (IPoolable poolable in objectPool)
        {
            if (poolable.GameObject.activeSelf)
            {
                activeCount++;
            }
        }
        return activeCount;
    }
    public List<GameObject> GetAllActiveObjects()
    {
        List<GameObject> activeObjects = new List<GameObject>();

        foreach (IPoolable poolable in objectPool)
        {
            if (poolable.GameObject.activeSelf)
            {
                activeObjects.Add(poolable.GameObject);
            }
        }
        return activeObjects;
    }
    public bool CheckIfPoolObjectIsAlreadyActive(IPoolable objectToCheck)
    {
        return objectToCheck.GameObject.activeSelf;
    }
}
