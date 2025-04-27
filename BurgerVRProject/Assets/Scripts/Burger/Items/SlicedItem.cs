using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedItem : BurgerItem
{
    [SerializeField] private SpawnableObject originalParent;
    
    public Action OnSlicedItemAttached;
    public Action OnSlicedItemDetached;
    
    public override void Attach(Transform parent, Vector3 pos, Quaternion rot)
    {
        base.Attach(parent, pos, rot);
        OnSlicedItemAttached?.Invoke();
    }

    private void OnDisable()
    {
        transform.SetParent(originalParent.transform);
        OnSlicedItemDetached?.Invoke();
    }
}
