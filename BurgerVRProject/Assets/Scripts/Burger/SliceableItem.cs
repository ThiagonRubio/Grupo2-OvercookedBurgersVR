using UnityEngine;
public class SliceableItem : MonoBehaviour, ISliceable
{
    public GameObject[] slicePrefabs;
    public void Slice()
    {
        for (int i = 0; i < slicePrefabs.Length; i++)
        {
            Instantiate(slicePrefabs[i], transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
