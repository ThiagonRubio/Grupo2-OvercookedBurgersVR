using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : PoolReturnManager
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colision con tacho");
        
        var manager = collision.gameObject.GetComponent<BurgerManager>();
        if (manager != null)
        {
            ReturnToPool(manager);
            return;
        }

        var slicedItem = collision.gameObject.GetComponent<SlicedItem>();
        if (slicedItem != null)
        {
            ReturnToPool(slicedItem);
            return;
        }

        var sliceableItem = collision.gameObject.GetComponent<SliceableItem>();
        if (sliceableItem != null)
        {
            ReturnToPool(sliceableItem);
            return;
        }
        
        var spawnableItem = collision.gameObject.GetComponent<SpawnableObject>();
        if (spawnableItem != null)
        {
            ReturnToPool(spawnableItem);
            return;
        }
    }

    protected override void ReturnToPool(BurgerManager manager)
    {
        base.ReturnToPool(manager);
    }
    protected override void ReturnToPool(SlicedItem item)
    {
        base.ReturnToPool(item);
    }
    protected override void ReturnToPool(SliceableItem item)
    {
        base.ReturnToPool(item);
    }
    protected override void ReturnToPool(SpawnableObject item)
    {
        base.ReturnToPool(item);
    }
}