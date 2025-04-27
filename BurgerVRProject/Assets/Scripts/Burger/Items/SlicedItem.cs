using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedItem : BurgerItem
{
    [SerializeField] private SpawnableObject originalParent;
    
    public Action OnSlicedItemAttached;
    public Action OnSlicedItemReattachedToOriginalParent;
    
    public override void Attach(Transform parent, Vector3 pos, Quaternion rot)
    {
        base.Attach(parent, pos, rot);
        OnSlicedItemAttached?.Invoke();
    }

    public void ReattachToOriginalParent()
    {
        transform.SetParent(originalParent.transform);
        OnSlicedItemReattachedToOriginalParent?.Invoke();
        gameObject.SetActive(false);
    }
}
