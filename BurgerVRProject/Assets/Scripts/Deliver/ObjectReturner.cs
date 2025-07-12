using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturner
{
    public virtual void ReturnToPool(Tray tray)
    {
        for (int i = tray.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = tray.transform.GetChild(i);

            if (child.TryGetComponent<SlicedItem>(out var sliced))
            {
                sliced.ReattachToOriginalParent();
            }
            else if (child.TryGetComponent<IPoolable>(out var poolable))
            {
                poolable.OnPoolableObjectDisable();
            }
        }

        DisablePoolableObject(tray.gameObject);
    }

    public virtual void ReturnToPool(SlicedItem item)
    {
        item.ReattachToOriginalParent();
        DisablePoolableObject(item.gameObject);
    }

    public virtual void ReturnToPool(SliceableItem item)
    {
        DisablePoolableObject(item.gameObject);
    }

    public virtual void ReturnToPool(SpawnableObject item)
    {
        DisablePoolableObject(item.gameObject);
    }

    public void DisablePoolableObject(GameObject obj)
    {
        if (obj.TryGetComponent<IPoolable>(out var poolable))
            poolable.OnPoolableObjectDisable();
        else
            obj.SetActive(false);
    }
}
