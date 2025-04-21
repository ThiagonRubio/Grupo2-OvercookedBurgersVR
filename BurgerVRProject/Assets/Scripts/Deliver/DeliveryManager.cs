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
        var poolables = new List<IPoolable>();
        foreach (Transform child in manager.transform)
        {
            var poolable =
              child.GetComponent<IPoolable>()
              ?? child.GetComponentInParent<IPoolable>(includeInactive: true);
            if (poolable != null)
                poolable.OnPoolableObjectDisable();
            else
                child.gameObject.SetActive(false);
        }

        var managerPoolable = manager.GetComponent<IPoolable>();
        if (managerPoolable != null)
            managerPoolable.OnPoolableObjectDisable();
        else
            manager.gameObject.SetActive(false);
    }
}
