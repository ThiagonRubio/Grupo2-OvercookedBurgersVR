using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : PoolReturnManager
{
    private void OnCollisionEnter(Collision collision)
    {
        var manager = collision.gameObject.GetComponent<BurgerManager>();
        if (manager != null)
            ReturnToPool(manager);
    }
    protected override void ReturnToPool(BurgerManager manager)
    {
        base.ReturnToPool(manager);
    }
}
