using UnityEngine;
public class Knife : MonoBehaviour
{
    private ISliceable _auxSliceable;
    private AudioSource cachedAudioSource;

    private void Awake()
    {
        cachedAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        _auxSliceable = collision.gameObject.GetComponent<ISliceable>();
        if (_auxSliceable != null)
        {
            _auxSliceable.Slice();
            cachedAudioSource.Play();
        }
    }
}
