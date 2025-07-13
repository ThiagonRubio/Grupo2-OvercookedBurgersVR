using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private AudioSource cachedAudioSource;
    private ObjectReturner cachedObjectReturner;

    private void Awake()
    {
        cachedAudioSource = GetComponent<AudioSource>();
        cachedObjectReturner = new ObjectReturner();
    }

    private void OnCollisionEnter(Collision collision)
    {
#if UNITY_EDITOR
        Debug.Log("Colision con tacho");
#endif

        var tray = collision.gameObject.GetComponent<Tray>();
        if (tray != null)
        {
            cachedObjectReturner.ReturnToPool(tray);
            cachedAudioSource.Play();
            return;
        }

        var slicedItem = collision.gameObject.GetComponent<SlicedItem>();
        if (slicedItem != null)
        {
            cachedObjectReturner.ReturnToPool(slicedItem);
            cachedAudioSource.Play();
            return;
        }

        var sliceableItem = collision.gameObject.GetComponent<SliceableObject>();
        if (sliceableItem != null)
        {
            cachedObjectReturner.ReturnToPool(sliceableItem);
            cachedAudioSource.Play();
            return;
        }
        
        var spawnableItem = collision.gameObject.GetComponent<SpawnableObject>();
        if (spawnableItem != null)
        {
            cachedObjectReturner.ReturnToPool(spawnableItem);
            cachedAudioSource.Play();
            return;
        }
    }
}