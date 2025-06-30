using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : MonoBehaviour
{
    private AudioSource cachedAudioSource;
    private int currentNumBeingCooked = 0;

    private void Awake()
    {
        cachedAudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cookable"))
        {
            currentNumBeingCooked++;
            if (!cachedAudioSource.isPlaying)
            {
                cachedAudioSource.Play();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cookable"))
        {
            currentNumBeingCooked--;

            if (currentNumBeingCooked <= 0)
            {
                cachedAudioSource.Stop();
            }
        }
    }
}
