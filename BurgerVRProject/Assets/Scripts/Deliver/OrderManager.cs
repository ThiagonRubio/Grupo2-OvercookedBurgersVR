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
    [SerializeField] private int minIngredients;
    [SerializeField] private int maxIngredients;
    private List<Order> currentOrders = new List<Order>();
    private int nextOrderId = 1;

    private void Start()
    {
        InvokeRepeating(nameof(GenerateOrder), timeBetweenOrders, timeBetweenOrders);
    }

    private void GenerateOrder()
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
        Debug.Log($"Nuevo pedido #{order.id}: {string.Join(", ", ingredients)}");
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

    public void RemoveOrder(List<IngredientType> burgerIngredients)
    {
        currentOrders.RemoveAll(o =>
        {
            if (o.ingredients.Count != burgerIngredients.Count) return false;
            var remaining = new List<IngredientType>(o.ingredients);
            foreach (var ing in burgerIngredients)
                if (remaining.Contains(ing)) remaining.Remove(ing);
                else return false;
            return true;
        });
    }
}
