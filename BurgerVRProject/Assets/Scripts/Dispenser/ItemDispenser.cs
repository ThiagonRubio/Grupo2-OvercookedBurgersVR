using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDispenser : MonoBehaviour
{
    public Transform ItemSpawnPoint => itemSpawnPosition;

    [SerializeField] protected SpawnableObject itemToSpawn;
    [SerializeField] private int poolSize = 10;
    [SerializeField] protected Transform itemSpawnPosition;

    private AudioSource cachedAudioSource;
    protected ObjectPool cachedItemPool;

    private void Awake()
    {
        cachedAudioSource = GetComponent<AudioSource>();

        if (itemToSpawn.TryGetComponent<IPoolable>(out IPoolable poolable))
        {
            cachedItemPool = new ObjectPool(itemToSpawn.GetComponent<IPoolable>(), poolSize);
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogWarning("Item dispenser can't create pool because Item to Spawn is not an IPoolable");
        }
#endif
    }

    public virtual void SpawnItem()
    {
        bool spawnSuccess = false;
        IPoolable item = cachedItemPool.TryGetPooledObject(out spawnSuccess);
        
        if (spawnSuccess)
        {
            item.OnPoolableObjectEnable();
            item.SetPoolablePositionAndRotation(itemSpawnPosition);
        }
        else
        {
            if (!cachedItemPool.IsPoolFull())
            {
                GameObject newObject = Instantiate(itemToSpawn.gameObject, transform.position, transform.rotation);
                newObject.name = transform.root.name + "_" + itemToSpawn.gameObject.name + "_" + cachedItemPool.GetQueueCount();
                newObject.transform.SetParent(gameObject.transform);
                IPoolable asPoolable = newObject.GetComponent<IPoolable>();
                asPoolable.OnPoolableObjectEnable();
                asPoolable.SetPoolablePositionAndRotation(itemSpawnPosition);
                cachedItemPool.EnqueuePoolable(asPoolable);
            }
        }
    }
}
