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
        else Debug.LogWarning("Object is not a poolable object.");
    }

    public IPoolable TryGetPooledObject(Transform spawnTransform = null)
    {
        IPoolable pooledObject = null;

        if (objectPool.Count < poolSize)
        {
            pooledObject = NewObject(spawnTransform);
        }
        else
        {
            pooledObject = ReuseObject(spawnTransform);
        }

        objectPool.Enqueue(pooledObject);
        return pooledObject;
    }

    private IPoolable NewObject(Transform spawnTransform = null)
    {
        var spawnPoint = spawnTransform != null ? spawnTransform : transform;
        
        GameObject newObject = Instantiate(objectToPool.GameObject, spawnPoint.position, spawnPoint.rotation);
        IPoolable pooledObject = newObject.GetComponent<IPoolable>();
        pooledObject.GameObject.name = transform.root.name + "_" + objectToPool.GameObject.name + "_" + objectPool.Count;
        pooledObject.GameObject.transform.SetParent(gameObject.transform);

        return pooledObject;
    }
    private IPoolable ReuseObject(Transform spawnTransform = null)
    {
        var spawnPoint = spawnTransform != null ? spawnTransform : transform;
        
        IPoolable pooledObject = objectPool.Dequeue();
        pooledObject.GameObject.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        pooledObject.GameObject.SetActive(true);

        return pooledObject;
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
