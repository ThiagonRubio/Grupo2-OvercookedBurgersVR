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
    // public List<Order> CurrentOrders => currentOrders;
    
    [SerializeField] private float timeBetweenOrders;
    [SerializeField] private int maxOrders;
    [SerializeField] private int minIngredients;
    [SerializeField] private int maxIngredients;
    private List<Order> currentOrders = new List<Order>();
    private int nextOrderId = 1;
    public Action<Order> OrderCreated;
    public Action<int> OrderRemoved;

    private void Start()
    {
        InvokeRepeating(nameof(GenerateOrder), 0, timeBetweenOrders);
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
        }
    }

    public bool OrderExists(List<IngredientType> burgerIngredients)
    {
        foreach (var order in currentOrders)
        {
            if (order.ingredients.Count != burgerIngredients.Count) continue;
            var remaining = new List<IngredientType>(order.ingredients);
            bool match = true;
            foreach (var ing in burgerIngredients)
            {
                if (remaining.Contains(ing)) remaining.Remove(ing);
                else { match = false; break; }
            }
            if (match) return true;
        }
        return false;
    }

    public bool OrderExistsOrdered(List<IngredientType> burgerIngredients)
    {
        return currentOrders.Any(o => o.ingredients.SequenceEqual(burgerIngredients));
    }

    public void RemoveOrder(List<IngredientType> burgerIngredients)
    {
        var toRemove = currentOrders.FirstOrDefault(o =>
        {
            if (o.ingredients.Count != burgerIngredients.Count) return false;
            var remaining = new List<IngredientType>(o.ingredients);
            foreach (var ing in burgerIngredients)
                if (remaining.Contains(ing)) remaining.Remove(ing);
                else return false;
            return true;
        });

        if (toRemove != null)
        {
            currentOrders.Remove(toRemove);
            OrderRemoved?.Invoke(toRemove.id);
        }
    }
}
