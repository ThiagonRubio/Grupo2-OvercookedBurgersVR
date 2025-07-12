using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private OrderManager orderManager;
    private ObjectReturner cachedObjectReturner;
    public static event Action<bool> OnOrderDelivered;

    private void Awake()
    {
        cachedObjectReturner = new ObjectReturner();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var tray = collision.gameObject.GetComponent<Tray>();
        if (tray == null) return;
        List<IngredientType> ingredients = new List<IngredientType>(tray.AttachedIngredients.Values);

        int orderId;
        bool isOrdered;
        if (orderManager.OrderExists(ingredients, out orderId, out isOrdered))
        {
            OnOrderDelivered?.Invoke(isOrdered);
            orderManager.RemoveOrderById(orderId);
            cachedObjectReturner.ReturnToPool(tray);
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Pedido no válido o inexistente");
#endif
        }
    }
}
