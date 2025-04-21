using System;
using UnityEngine;
public class SliceableItem : MonoBehaviour, ISliceable
{
    [SerializeField] private GameObject[] cutParts;

    public void Slice()
    {
        for (int i = 0; i < cutParts.Length; i++)
        {
            cutParts[i].transform.localPosition = new Vector3(gameObject.transform.localPosition.x, (gameObject.transform.localPosition.y + (i * 0.015f)), gameObject.transform.localPosition.z);
            cutParts[i].SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
