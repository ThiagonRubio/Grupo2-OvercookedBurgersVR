using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Order
{
    public int id;
    public List<IngredientType> ingredients;
}

public class OrderManager : MonoBehaviour
{    
    [SerializeField] private float timeBetweenOrders;
    [SerializeField] private float baseTimeBetweenOrders;
    [SerializeField] private float extraTimePerIngredient;
    [SerializeField] private int maxOrders;
    [SerializeField] private int minIngredients;
    [SerializeField] private int maxIngredients;

    private List<Order> currentOrders = new List<Order>();
    private int nextOrderId = 1;

    public Action<Order> OrderCreated;
    public Action<int> OrderRemoved;

    private void OnEnable()
    {
        RegisterUI.OnRegisterUIToggled += StartGeneratingOrders;
    }

    private void OnDisable()
    {
        RegisterUI.OnRegisterUIToggled -= StartGeneratingOrders;
    }

    private void StartGeneratingOrders()
    {
        GenerateOrder();
    }

    private void GenerateOrder()
    {
        if (currentOrders.Count < maxOrders)
        {
            int midCount = UnityEngine.Random.Range(minIngredients, maxIngredients + 1);
            List<IngredientType> ingredients = new List<IngredientType>();
            ingredients.Add(IngredientType.PanInferior);

            IngredientType[] allTypes = (IngredientType[])Enum.GetValues(typeof(IngredientType));
            List<IngredientType> pool = allTypes
                .Where(i => i != IngredientType.PanInferior && i != IngredientType.PanSuperior)
                .ToList();

            for (int i = 0; i < midCount; i++)
                ingredients.Add(pool[UnityEngine.Random.Range(0, pool.Count)]);

            ingredients.Add(IngredientType.PanSuperior);

            Order order = new Order { id = nextOrderId++, ingredients = ingredients };
            currentOrders.Add(order);
            OrderCreated?.Invoke(order);

            timeBetweenOrders = baseTimeBetweenOrders + extraTimePerIngredient * midCount;
            Invoke(nameof(GenerateOrder), timeBetweenOrders);

        }
        else
        {
            Invoke(nameof(GenerateOrder), 0.1f);
        }
    }

    public bool OrderExists(List<IngredientType> burgerIngredients, out int orderId, out bool isOrdered)
    {
        foreach (var order in currentOrders)
        {
            if (order.ingredients.Count != burgerIngredients.Count) continue;

            var remaining = new List<IngredientType>(order.ingredients);
            bool unorderedMatch = true;
            foreach (var ing in burgerIngredients)
            {
                if (remaining.Contains(ing)) remaining.Remove(ing);
                else { unorderedMatch = false; break; }
            }
            if (unorderedMatch)
            {
                orderId = order.id;
                isOrdered = order.ingredients.SequenceEqual(burgerIngredients);
                return true;
            }
        }
        orderId = -1;
        isOrdered = false;
        return false;
    }
    public void RemoveOrderById(int orderId)
    {
        var toRemove = currentOrders.FirstOrDefault(o => o.id == orderId);
        if (toRemove != null)
        {
            currentOrders.Remove(toRemove);
            OrderRemoved?.Invoke(orderId);

            if (currentOrders.Count == 0)
            {
                CancelInvoke(nameof(GenerateOrder));
                GenerateOrder();
            }
        }
    }
}