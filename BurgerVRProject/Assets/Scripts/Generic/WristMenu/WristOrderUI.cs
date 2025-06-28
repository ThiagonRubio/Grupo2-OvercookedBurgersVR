using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristOrderUI : MonoBehaviour
{
    [SerializeField] private IngredientImages ingredientImages;
    [SerializeField] private Transform imagesLayoutGroup;
    [SerializeField] private Image timerFillImage;

    public Order currentOrder;
    private OrderUI trackedOrderUI;
    private float baseOrderDuration = 10f;
    private float extraOrderDuration = 5f;
    private float totalDuration;
    private Coroutine uiRoutine;

    public void SetOrder(Order order, OrderUI orderUI)
    {
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

        if (uiRoutine != null)
            StopCoroutine(uiRoutine);
        uiRoutine = StartCoroutine(UpdateBarRoutine());
    }

    private IEnumerator UpdateBarRoutine()
    {
        while (trackedOrderUI != null && trackedOrderUI.CurrentTimeLeft > 0f)
        {
            float percent = trackedOrderUI.TotalDuration > 0f
                ? Mathf.Clamp01(trackedOrderUI.CurrentTimeLeft / trackedOrderUI.TotalDuration)
                : 0f;
            if (timerFillImage != null)
            {
                timerFillImage.fillAmount = percent;
                timerFillImage.color = GetColorForPercent(percent);
            }
            yield return new WaitForSeconds(0.05f);
        }
        if (timerFillImage != null)
        {
            timerFillImage.fillAmount = 0f;
            timerFillImage.color = GetColorForPercent(0f);
        }
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
