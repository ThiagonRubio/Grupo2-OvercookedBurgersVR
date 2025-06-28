using UnityEngine;
public class Knife : MonoBehaviour
{
    private ISliceable _auxSliceable;
    void OnCollisionEnter(Collision collision)
    {
        _auxSliceable = collision.gameObject.GetComponent<ISliceable>();
        if (_auxSliceable != null)
            _auxSliceable.Slice();
    }
}
