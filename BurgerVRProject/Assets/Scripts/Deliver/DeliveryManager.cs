using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var manager = collision.gameObject.GetComponent<BurgerManager>();
        if (manager != null)
            ReturnToPool(manager);
    }
    private void ReturnToPool(BurgerManager manager)
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
                
                if(slicedItem != null)
                    slicedItem.ReattachToOriginalParent();
            }
        }
        
        var managerPoolable = manager.GetComponent<IPoolable>();
        if (managerPoolable != null)
            managerPoolable.OnPoolableObjectDisable();
        else
            manager.gameObject.SetActive(false);
    }
}
