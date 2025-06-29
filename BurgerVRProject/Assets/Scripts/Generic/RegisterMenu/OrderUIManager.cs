using System.Linq;
using UnityEngine;

public class OrderUIManager : MonoBehaviour
{
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private OrderUI[] orderUIs;
    private WristOrderUI wristOrderUI;

    private void Start()
    {
        orderManager = FindFirstObjectByType<OrderManager>();
        orderUIs = GetComponentsInChildren<OrderUI>(true);
        wristOrderUI = FindAnyObjectByType<WristOrderUI>();

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
        if(wristOrderUI == null)
            wristOrderUI = FindAnyObjectByType<WristOrderUI>();

        for (int i = 0; i < orderUIs.Length; i++)
        {
            if (!orderUIs[i].gameObject.activeSelf)
            {
                orderUIs[i].gameObject.SetActive(true);
                orderUIs[i].SetOrder(order);

                if (wristOrderUI != null)
                {
                    float newOrderDuration = orderUIs[i].CurrentTimeLeft;
                    bool hasCurrent = wristOrderUI.HasCurrentOrder();
                    float currentOrderTimeLeft = hasCurrent ? wristOrderUI.GetCurrentOrderTimeLeft() : float.MaxValue;

                    if (!hasCurrent || newOrderDuration < currentOrderTimeLeft)
                    {
                        wristOrderUI.gameObject.SetActive(true);
                        wristOrderUI.SetOrder(order, orderUIs[i]);
                    }
                }
                return;
            }
        }

#if UNITY_EDITOR
        Debug.LogWarning("No hay mas espacios libres para mostrar nuevas ordenes.");
#endif
    }

    private void OnOrderRemoved(int orderId)
    {
        for (int i = 0; i < orderUIs.Length; i++)
        {
            if (orderUIs[i].CurrentOrderId == orderId)
            {
                if(wristOrderUI != null)
                {
                    bool wasWristOrder = wristOrderUI.currentOrder.id == orderUIs[i].CurrentOrderId;

                    if (wasWristOrder)
                    {
                        OrderUI minOrderUI = null;
                        float minTime = float.MaxValue;

                        for (int j = 0; j < orderUIs.Length; j++)
                        {
                            if (orderUIs[j].gameObject.activeSelf && orderUIs[j].CurrentTimeLeft > 0f)
                            {
                                if (orderUIs[j].CurrentTimeLeft < minTime)
                                {
                                    minTime = orderUIs[j].CurrentTimeLeft;
                                    minOrderUI = orderUIs[j];
                                }
                            }
                        }

                        if (minOrderUI != null)
                        {
                            wristOrderUI.SetOrder(minOrderUI.CurrentOrder, minOrderUI);
                        }
                        else
                        {
                            wristOrderUI.gameObject.SetActive(false);
                        }
                    }
                }

                orderUIs[i].gameObject.SetActive(false);
                orderUIs[i].transform.SetAsLastSibling();
                return;
            }
        }
    }
}