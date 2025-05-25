using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class IngredientImagePair
{
    public IngredientType ingredientType;
    public Sprite ingredientSprite;
}

public class OrderUI : MonoBehaviour
{
    [SerializeField] private IngredientImages ingredientImages;
    [SerializeField] private Transform imagesLayoutGroup;
    [SerializeField] private TextMeshProUGUI orderText;
    public int CurrentOrderId { get; private set; }

    public void SetOrder(Order order)
    {
        CurrentOrderId = order.id;

        for (int i = 0; i < imagesLayoutGroup.childCount; i++)
            imagesLayoutGroup.GetChild(i).gameObject.SetActive(false);

        for (int i = 0; i < order.ingredients.Count && i < imagesLayoutGroup.childCount; i++)
        {
            var ingredientType = order.ingredients[i];
            var child = imagesLayoutGroup.GetChild(i).gameObject;
            child.SetActive(true);

            var image = child.GetComponent<Image>();
            var sprite = ingredientImages.ingredientImagesList
                .Find(x => x.ingredientType == ingredientType)?.ingredientSprite;
            image.sprite = sprite;
        }
        orderText.text = $"#{order.id}";
        Debug.Log($"Nuevo pedido #{order.id}: {string.Join(", ", order.ingredients)}");
    }
}