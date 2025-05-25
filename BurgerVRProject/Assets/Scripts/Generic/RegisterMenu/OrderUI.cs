using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class IngredientImagePair
{
    public IngredientType ingredientType;
    public Sprite ingredientSprite;
}

public class OrderUI : MonoBehaviour
{
    private OrderManager _orderManager;
    [SerializeField] private IngredientImages ingredientImages;
    [SerializeField] private Transform imagesLayoutGroup;
    private void Start()
    {
        _orderManager = FindFirstObjectByType<OrderManager>();
        _orderManager.OrderCreated += OnOrderCreated;
    }

    private void OnDisable()
    {
        _orderManager.OrderCreated -= OnOrderCreated;
    }

    private void OnOrderCreated(Order order)
    {
        Debug.Log($"Nuevo pedido #{order.id}: {string.Join(", ", order.ingredients)}");

        // for (int i = 0; i <= order.ingredients.Count; i++)
        // {
        //     imagesLayoutGroup.GetChild(i).gameObject.SetActive(true);
        //     imagesLayoutGroup.GetChild(i).GetComponent<Image>().sprite = ingredientImages.ingredientImagesList.Find(e => e.ingredientType == order.ingredients[i])
        // }
    }
}
