using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolReturnManager : MonoBehaviour
{
    protected virtual void ReturnToPool(BurgerManager manager)
    {
        for (int i = manager.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = manager.transform.GetChild(i);
            var poolable = child.GetComponent<IPoolable>();
            if (poolable != null)
            {
                poolable.OnPoolableObjectDisable();
            }
            else
            {
                var slicedItem = child.GetComponent<SlicedItem>();
                if (slicedItem != null)
                    slicedItem.ReattachToOriginalParent();
            }
        }
        DisablePoolable(manager.gameObject);
    }

    protected virtual void ReturnToPool(SlicedItem item)
    {
        item.ReattachToOriginalParent();
        DisablePoolable(item.gameObject);
    }
    protected virtual void ReturnToPool(SliceableItem item)
    {
        DisablePoolable(item.gameObject);
    }
    protected virtual void ReturnToPool(SpawnableObject item)
    {
        DisablePoolable(item.gameObject);
    }
    private void DisablePoolable(GameObject item)
    {
        var poolable = item.GetComponent<IPoolable>();
        if (poolable != null)
            poolable.OnPoolableObjectDisable();
        else
            item.gameObject.SetActive(false);
    }
}