using UnityEngine;
public class Knife : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        ISliceable sliceable = collision.gameObject.GetComponent<ISliceable>();
        if (sliceable != null)
            sliceable.Slice();
    }
}
