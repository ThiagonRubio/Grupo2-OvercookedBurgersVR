using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI finalOrdersDeliveredText;
    [SerializeField] private TextMeshProUGUI finalAngryCustomersText;
    [SerializeField] private ScoreManager scoreManager;

    private void OnEnable()
    {
        TimerManager.OnTimerEnded += ShowFinishScreen;
    }

    private void OnDisable()
    {
        TimerManager.OnTimerEnded -= ShowFinishScreen;
    }

    private void ShowFinishScreen()
    {
        gamePanel.SetActive(false);
        finishPanel.SetActive(true);
        if (scoreManager != null)
        {
            finalScoreText.text = "Score: " + scoreManager.GetScore();
            finalOrdersDeliveredText.text = "Orders Delivered: " + scoreManager.GetOrdersDelivered();
            finalAngryCustomersText.text = "Angry Customers: " + scoreManager.GetAngryCustomers();
        }
    }
}
