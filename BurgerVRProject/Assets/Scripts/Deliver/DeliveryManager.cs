using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : PoolReturnManager
{
    [SerializeField] private OrderManager orderManager;

    private void OnCollisionEnter(Collision collision)
    {
        var manager = collision.gameObject.GetComponent<BurgerManager>();
        if (manager == null) return;
        List<IngredientType> ingredients = new List<IngredientType>(manager.AttachedIngredients.Values);

        int orderId;
        bool isOrdered;
        if (orderManager.OrderExists(ingredients, out orderId, out isOrdered))
        {
            if (isOrdered)
                Debug.Log("Pedido entregado en orden CORRECTA");
            else
                Debug.Log("Pedido entregado CORRECTO pero DESORDENADO");

            orderManager.RemoveOrderById(orderId);
            ReturnToPool(manager);
        }
        else
        {
            Debug.Log("Pedido no válido o inexistente");
        }
    }

    protected override void ReturnToPool(BurgerManager manager)
    {
        base.ReturnToPool(manager);
    }
}
