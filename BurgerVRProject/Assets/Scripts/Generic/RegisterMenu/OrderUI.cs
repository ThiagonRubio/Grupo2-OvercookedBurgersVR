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
    [SerializeField] private TextMeshProUGUI orderText;
    [SerializeField] private Image timerFillImage;
    [SerializeField] private float orderDuration = 10f;
    [SerializeField] private int penaltyPoints = 3;

    public static event Action<int> OnOrderExpired;
    private Coroutine timerRoutine;
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
        if (timerRoutine != null) StopCoroutine(timerRoutine);
        timerRoutine = StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (true)
        {
            float timeLeft = orderDuration;
            while (timeLeft > 0f)
            {
                timeLeft -= Time.deltaTime;
                float percent = timeLeft / orderDuration;

                if (timerFillImage != null)
                {
                    timerFillImage.fillAmount = percent;
                    timerFillImage.color = GetColorForPercent(percent);
                }

                yield return null;
            }

            OnOrderExpired?.Invoke(penaltyPoints);
        }
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
