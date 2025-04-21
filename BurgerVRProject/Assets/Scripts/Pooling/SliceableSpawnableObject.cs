using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableSpawnableObject : SpawnableObject
{
    [SerializeField] private GameObject fullVersion;
    [SerializeField] private GameObject cutVersion;
    [SerializeField] private GameObject cutTop;
    [SerializeField] private GameObject cutBottom;
    
    public override void OnPoolableObjectEnable()
    {
        base.OnPoolableObjectEnable();
        cutTop.transform.localPosition = new Vector3(0, 0.015f, 0);
        cutBottom.transform.localPosition = Vector3.zero;
        fullVersion.transform.localPosition = Vector3.zero;
        cutVersion.SetActive(false);
        fullVersion.SetActive(true);
    }
}
