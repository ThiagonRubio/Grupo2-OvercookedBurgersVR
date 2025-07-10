using TMPro;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int normalScore = 10;
    [SerializeField] private int perfectScore = 5;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ordersDeliveredText;
    [SerializeField] private TextMeshProUGUI angryCustomersText;
    private AudioSource cachedAudioSource;

    private int currentScore;
    private int currentsOrdersDelivered;
    private int angryCustomers;

    public static event Action<int> OnScoreChanged;

    private void Awake()
    {
        cachedAudioSource = GetComponent<AudioSource>();
    }

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
        cachedAudioSource.Play();
        UpdateScoreDisplay();
        OnScoreChanged?.Invoke(currentScore);
    }

    public void ReduceScore(int amount)
    {
        currentScore -= amount;
        angryCustomers += amount;
        if (currentScore < 0) currentScore = 0;
        UpdateScoreDisplay();
        OnScoreChanged?.Invoke(currentScore);
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
            scoreText.text = "$" + currentScore.ToString();

        if (ordersDeliveredText != null)
            ordersDeliveredText.text = "Orders Delivered: " + currentsOrdersDelivered.ToString();

        if (angryCustomersText != null)
            angryCustomersText.text = "Angry Customers: " + angryCustomers.ToString();
    }

    public int GetScore() => currentScore;
    public int GetOrdersDelivered() => currentsOrdersDelivered;
    public int GetAngryCustomers() => angryCustomers;
}
