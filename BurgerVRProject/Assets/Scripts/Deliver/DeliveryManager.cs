using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : PoolReturnManager
{
    [SerializeField] private OrderManager orderManager;
    public static event Action<bool> OnOrderDelivered;

    private void OnCollisionEnter(Collision collision)
    {
        var manager = collision.gameObject.GetComponent<BurgerManager>();
        if (manager == null) return;
        List<IngredientType> ingredients = new List<IngredientType>(manager.AttachedIngredients.Values);

        int orderId;
        bool isOrdered;
        if (orderManager.OrderExists(ingredients, out orderId, out isOrdered))
        {
            OnOrderDelivered?.Invoke(isOrdered);
            orderManager.RemoveOrderById(orderId);
            ReturnToPool(manager);
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Pedido no válido o inexistente");
#endif
        }
    }
}
