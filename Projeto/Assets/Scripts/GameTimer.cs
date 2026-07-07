using System;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float startTime = 60f;

    private float currentTime;
    private bool isRunning;

    public event Action OnTimeUp;

    private void Start()
    {
        currentTime = GameManager.Instance != null ? GameManager.Instance.roundTime : startTime;
        isRunning = true;
        UpdateText();
    }

    private void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isRunning = false;
            UpdateText();
            OnTimeUp?.Invoke();
            return;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        if (timerText == null) return;
        int seconds = Mathf.CeilToInt(currentTime);
        timerText.text = seconds.ToString();
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
