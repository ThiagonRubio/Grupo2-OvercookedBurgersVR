using UnityEngine;
public class Knife : MonoBehaviour
{
    private SliceableObject _auxSliceable;
    private AudioSource cachedAudioSource;

    private void Awake()
    {
        cachedAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        _auxSliceable = collision.transform.parent.GetComponent<SliceableObject>();
        if (_auxSliceable != null && !_auxSliceable.Cut)
        {
            _auxSliceable.Slice();
            cachedAudioSource.Play();
        }
    }
}
