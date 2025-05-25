using UnityEngine;

public class OrderUIManager : MonoBehaviour
{
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private OrderUI[] orderUIs;

    private void Start()
    {
        orderManager = FindFirstObjectByType<OrderManager>();
        orderUIs = GetComponentsInChildren<OrderUI>(true);
        orderManager.OrderCreated += OnOrderCreated;
        orderManager.OrderRemoved += OnOrderRemoved;
    }

    private void OnDestroy()
    {
        orderManager.OrderCreated -= OnOrderCreated;
        orderManager.OrderRemoved -= OnOrderRemoved;
    }

    private void OnOrderCreated(Order order)
    {
        for (int i = 0; i < orderUIs.Length; i++)
        {
            if (!orderUIs[i].gameObject.activeSelf)
            {
                orderUIs[i].gameObject.SetActive(true);
                orderUIs[i].SetOrder(order);
                return;
            }
        }
        Debug.LogWarning("No hay más espacios libres para mostrar nuevas órdenes.");
    }

    private void OnOrderRemoved(int orderId)
    {
        for (int i = 0; i < orderUIs.Length; i++)
        {
            if (orderUIs[i].CurrentOrderId == orderId)
            {
                orderUIs[i].gameObject.SetActive(false);
                return;
            }
        }
    }

}