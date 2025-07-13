using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableObject : SpawnableObject
{
    public bool Cut => _cut;
    
    [SerializeField] private GameObject fullVersion;
    [SerializeField] private SlicedItem[] cutParts;

    private bool _cut;
    
    public override void OnPoolableObjectEnable()
    {
        base.OnPoolableObjectEnable();

        for (int i = 0; i < cutParts.Length; i++)
        {
            cutParts[i].OnSlicedItemAttached += OnChildDetached;
            cutParts[i].OnSlicedItemReattachedToOriginalParent += CheckPoolAvailability;
            
            cutParts[i].transform.localPosition = new Vector3(0, (i * 0.015f), 0);
            cutParts[i].transform.localRotation = Quaternion.Euler(0,0,0);
            cutParts[i].gameObject.SetActive(false);
        }
        
        fullVersion.transform.localPosition = Vector3.zero;
        fullVersion.transform.localRotation = Quaternion.Euler(0,0,0);
        fullVersion.SetActive(true);
        
        _cut = false;
    }

    public override void OnPoolableObjectDisable()
    {
        base.OnPoolableObjectDisable();
        
        for (int i = 0; i < cutParts.Length; i++)
        {
            cutParts[i].OnSlicedItemAttached -= OnChildDetached;
            cutParts[i].OnSlicedItemReattachedToOriginalParent -= CheckPoolAvailability;
        }

        _cut = false;
    }

    private void OnChildDetached()
    {
        //Sabe que si se acaba de ir su child ya no puede poolearse
        IsAvailable = false;
    }
    
    private void CheckPoolAvailability()
    {
        //Si ningún hijo está siendo utilizado (está en otro parent), está completo y puede poolearse
        
        IsAvailable = true;
        foreach (var item in cutParts)
        {
            if (item.transform.parent != gameObject.transform)
            {
                IsAvailable = false;
            }
        }
    }
    
    public void Slice()
    {
        for (int i = 0; i < cutParts.Length; i++)
        {
            cutParts[i].transform.localPosition = new Vector3(fullVersion.transform.localPosition.x, (fullVersion.transform.localPosition.y + (i * 0.015f)), fullVersion.transform.localPosition.z);
            cutParts[i].gameObject.SetActive(true);
        }
        fullVersion.SetActive(false);
        _cut = true;
    }
}
