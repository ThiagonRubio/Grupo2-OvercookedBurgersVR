using System;
using UnityEngine;
public class SliceableItem : MonoBehaviour, ISliceable
{
    [SerializeField] private GameObject cutVersion;

    public void Slice()
    {
        cutVersion.transform.localPosition = gameObject.transform.localPosition;
        cutVersion.SetActive(true);
        gameObject.SetActive(false);
    }
}
