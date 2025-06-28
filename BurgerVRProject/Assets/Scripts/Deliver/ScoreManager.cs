using TMPro;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int normalScore = 10;
    [SerializeField] private int perfectScore = 5;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ordersDeliveredText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float gameDurationSeconds = 300f;

    private int currentScore;
    private int currentsOrdersDelivered;

    private float remainingTime;
    private bool timerActive;

    public static event Action OnTimerEnded;
    public event Action<float> OnTimerTick;

    private void OnEnable()
    {
        DeliveryManager.OnOrderDelivered += HandleOrderDelivered;
        OrderUI.OnOrderExpired += ReduceScore;
        RegisterUI.OnRegisterUIToggled += StartTimer;
        UpdateScoreDisplay();
        UpdateTimerDisplay();
    }

    private void OnDisable()
    {
        DeliveryManager.OnOrderDelivered -= HandleOrderDelivered;
        OrderUI.OnOrderExpired -= ReduceScore;
        RegisterUI.OnRegisterUIToggled -= StartTimer;

    }

    private void StartTimer()
    {
        remainingTime = gameDurationSeconds;
        timerActive = true;
        InvokeRepeating(nameof(TimerTick), 1f, 1f);
    }

    private void TimerTick()
    {
        if (!timerActive) return;
        remainingTime -= 1f;
        UpdateTimerDisplay();
        OnTimerTick?.Invoke(remainingTime);

        if (remainingTime <= 0f)
        {
            timerActive = false;
            CancelInvoke(nameof(TimerTick));
            OnTimerEnded?.Invoke();
        }
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
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);
            timerText.text = $"{minutes}:{seconds.ToString("00")}";
        }
    }
}