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

            if (child.TryGetComponent<SlicedItem>(out var sliced))
            {
                sliced.ReattachToOriginalParent();
            }
            else if (child.TryGetComponent<IPoolable>(out var poolable))
            {
                poolable.OnPoolableObjectDisable();
            }
        }

        DisablePoolableObject(manager.gameObject);
    }

    protected virtual void ReturnToPool(SlicedItem item)
    {
        item.ReattachToOriginalParent();
        DisablePoolableObject(item.gameObject);
    }

    protected virtual void ReturnToPool(SliceableItem item)
    {
        DisablePoolableObject(item.gameObject);
    }

    protected virtual void ReturnToPool(SpawnableObject item)
    {
        DisablePoolableObject(item.gameObject);
    }

    protected void DisablePoolableObject(GameObject obj)
    {
        if (obj.TryGetComponent<IPoolable>(out var poolable))
            poolable.OnPoolableObjectDisable();
        else
            obj.SetActive(false);
    }
}
