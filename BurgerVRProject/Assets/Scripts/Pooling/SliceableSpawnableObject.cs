using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableSpawnableObject : SpawnableObject
{
    [SerializeField] private GameObject fullVersion;
    [SerializeField] private GameObject[] cutParts;
    
    
    
    public override void OnPoolableObjectEnable()
    {
        base.OnPoolableObjectEnable();

        for (int i = 0; i < cutParts.Length; i++)
        {
            cutParts[i].transform.localPosition = new Vector3(0, (i * 0.015f), 0);
            cutParts[i].transform.localRotation = Quaternion.Euler(0,0,0);
            cutParts[i].SetActive(false);
        }
        
        fullVersion.transform.localPosition = Vector3.zero;
        fullVersion.transform.localRotation = Quaternion.Euler(0,0,0);
        fullVersion.SetActive(true);
    }
}
