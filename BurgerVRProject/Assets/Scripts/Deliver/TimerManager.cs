using TMPro;
using UnityEngine;
using System;

public class TimerManager : MonoBehaviour, IUpdatable
{
    [SerializeField] private float gameDurationSeconds = 300f;
    [SerializeField] private TextMeshProUGUI timerText;

    private float remainingTime;
    private bool timerActive;
    private int lastDisplayedSeconds = -1;

    public static event Action OnTimerEnded;

    private void OnEnable()
    {
        RegisterUI.OnRegisterUIToggled += StartTimer;
    }

    private void OnDisable()
    {
        RegisterUI.OnRegisterUIToggled -= StartTimer;
        if (CustomUpdateManager.Instance != null)
            CustomUpdateManager.Instance.Unregister(this);
    }

    private void StartTimer()
    {
        remainingTime = gameDurationSeconds;
        timerActive = true;
        lastDisplayedSeconds = Mathf.CeilToInt(remainingTime);
        CustomUpdateManager.Instance.Register(this);
        UpdateTimerDisplay();
    }

    public void OnUpdate()
    {
        if (!timerActive) return;

        remainingTime -= Time.deltaTime;

        int currentSeconds = Mathf.CeilToInt(remainingTime);
        if (currentSeconds != lastDisplayedSeconds)
        {
            lastDisplayedSeconds = currentSeconds;
            UpdateTimerDisplay();
        }

        if (remainingTime <= 0f)
        {
            timerActive = false;
            CustomUpdateManager.Instance.Unregister(this);
            remainingTime = 0f;
            UpdateTimerDisplay();
            OnTimerEnded?.Invoke();
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.Clamp(Mathf.CeilToInt(remainingTime % 60f), 0, 59);
        if (timerText != null)
            timerText.text = $"{minutes}:{seconds.ToString("00")}";
    }
}
