using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : PoolReturnManager
{
    private AudioSource cachedAudioSource;

    private void Awake()
    {
        cachedAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
#if UNITY_EDITOR
        Debug.Log("Colision con tacho");
#endif

        var manager = collision.gameObject.GetComponent<BurgerManager>();
        if (manager != null)
        {
            ReturnToPool(manager);
            cachedAudioSource.Play();
            return;
        }

        var slicedItem = collision.gameObject.GetComponent<SlicedItem>();
        if (slicedItem != null)
        {
            ReturnToPool(slicedItem);
            cachedAudioSource.Play();
            return;
        }

        var sliceableItem = collision.gameObject.GetComponent<SliceableItem>();
        if (sliceableItem != null)
        {
            ReturnToPool(sliceableItem);
            cachedAudioSource.Play();
            return;
        }
        
        var spawnableItem = collision.gameObject.GetComponent<SpawnableObject>();
        if (spawnableItem != null)
        {
            ReturnToPool(spawnableItem);
            cachedAudioSource.Play();
            return;
        }
    }
}