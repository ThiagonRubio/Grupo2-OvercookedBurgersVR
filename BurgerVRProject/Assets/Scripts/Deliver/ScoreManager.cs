using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int normalScore = 10;
    [SerializeField] private int perfectScore = 5;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ordersDeliveredText;

    private int currentScore;
    private int currentsOrdersDelivered;

    private void OnEnable()
    {
        DeliveryManager.OnOrderDelivered += HandleOrderDelivered;
        OrderUI.OnOrderExpired += ReduceScore;
        UpdateScoreDisplay();
    }

    private void OnDisable()
    {
        DeliveryManager.OnOrderDelivered -= HandleOrderDelivered;
        OrderUI.OnOrderExpired -= ReduceScore;
    }
    private void HandleOrderDelivered(bool isOrdered)
    {
        currentScore += isOrdered ? normalScore : perfectScore;
        currentsOrdersDelivered++;
        UpdateScoreDisplay();
    }

    public void ReduceScore(int amount)
    {
        currentScore -= amount;
        if (currentScore < 0) currentScore = 0;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        Debug.Log("score actual: " + currentScore);

        if (scoreText != null)
            scoreText.text = "$" + currentScore.ToString();

        if(ordersDeliveredText != null)
            ordersDeliveredText.text = "Orders Delivered: " + currentsOrdersDelivered.ToString();

    }
}
