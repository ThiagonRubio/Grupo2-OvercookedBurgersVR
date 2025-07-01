using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristOrderUI : MonoBehaviour
{
    [SerializeField] private IngredientImages ingredientImages;
    [SerializeField] private Transform imagesLayoutGroup;

    public Order currentOrder;
    private OrderUI trackedOrderUI;
    private float baseOrderDuration = 10f;
    private float extraOrderDuration = 5f;
    private float totalDuration;
    private Coroutine uiRoutine;

    private void OnEnable()
    {
        ScoreManager.OnTimerEnded += HandleTimerEnded;
    }

    private void OnDisable()
    {
        ScoreManager.OnTimerEnded -= HandleTimerEnded;
    }

    public void SetOrder(Order order, OrderUI orderUI)
    {
        imagesLayoutGroup.gameObject.SetActive(true);

        currentOrder = order;
        trackedOrderUI = orderUI;

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

        totalDuration = baseOrderDuration + order.ingredients.Count * extraOrderDuration;
    }

    public bool HasCurrentOrder()
    {
        return currentOrder != null && trackedOrderUI != null && trackedOrderUI.CurrentTimeLeft > 0f;
    }

    public float GetCurrentOrderTimeLeft()
    {
        if (trackedOrderUI == null) return 0f;
        return trackedOrderUI.CurrentTimeLeft;
    }

    private void HandleTimerEnded()
    {
        imagesLayoutGroup.gameObject.SetActive(false);
    }
}
