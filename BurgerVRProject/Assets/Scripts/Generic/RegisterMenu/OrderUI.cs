using System;
using System.Collections;
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
    [SerializeField] private Image timerFillImage;
    [SerializeField] private float baseOrderDuration = 10f;
    [SerializeField] private float extraOrderDuration = 5f;
    [SerializeField] private int penaltyPoints = 3;
    private Order currentOrder;
    public Order CurrentOrder => currentOrder;

    public static event Action<int> OnOrderExpired;
    private Coroutine timerRoutine;
    public int CurrentOrderId { get; private set; }
    public float CurrentTimeLeft { get; private set; }
    public float TotalDuration { get; private set; }
    public void SetOrder(Order order)
    {
        CurrentOrderId = order.id;
        currentOrder = order;
    
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

        TotalDuration = baseOrderDuration + order.ingredients.Count * extraOrderDuration;

        if (timerRoutine != null) StopCoroutine(timerRoutine);
        timerRoutine = StartCoroutine(StartTimer(TotalDuration));
    }

    private IEnumerator StartTimer(float duration)
    {
        CurrentTimeLeft = duration;

        while (CurrentTimeLeft > 0f)
        {
            CurrentTimeLeft -= Time.deltaTime;
            float percent = CurrentTimeLeft / duration;

            if (timerFillImage != null)
            {
                timerFillImage.fillAmount = percent;
                timerFillImage.color = GetColorForPercent(percent);
            }

            yield return null;
        }

        OnOrderExpired?.Invoke(penaltyPoints);
        StartCoroutine(StartTimer(duration));
    }

    private Color GetColorForPercent(float percent)
    {
        if (percent > 0.75f)
            return Color.green;
        else if (percent > 0.5f)
            return Color.yellow;
        else if (percent > 0.25f)
            return new Color(1f, 0.5f, 0f);
        else
            return Color.red;
    }
}
