using System;
using TMPro;
using UnityEngine;

public class NumericClock : MonoBehaviour
{
    [SerializeField] private Ticker ticker;
    [SerializeField] private TMP_InputField inputField;
    
    private DateTime currentDateTime;

    private void Awake()
    {
        ticker.OnTimeSecondTick += AddSeconds;
        ticker.OnTimeSet += SetTime;
        inputField.onSelect.AddListener(_ => ticker.CanTick = false);
        inputField.onSubmit.AddListener(_ => TrySetTimeFromInputField());
    }

    private void OnDisable()
    {
        ticker.OnTimeSecondTick -= AddSeconds;
        ticker.OnTimeSet -= SetTime;
        inputField.onSelect.RemoveAllListeners();
    }

    private void AddSeconds(float deltaTime)
    {
        currentDateTime = currentDateTime.AddSeconds(deltaTime);
        inputField.text = currentDateTime.Hour + ":" + currentDateTime.Minute + ":" + currentDateTime.Second;
    }

    private void TrySetTimeFromInputField()
    {
        try
        {
            var strings = inputField.text.Split(':');
            var dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                int.Parse(strings[0]), int.Parse(strings[1]), int.Parse(strings[2]));
            ticker.SynchronizeTime(dateTime);
        }
        
        catch (Exception e)
        {
            Debug.LogWarning("Неправильный формат ввода");
        }
    }

    private void SetTime(DateTime dateTime)
    {
        currentDateTime = new DateTime(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            dateTime.Hour,
            dateTime.Minute,
            dateTime.Second,
            dateTime.Millisecond);
        
        inputField.text = currentDateTime.Hour + ":" + currentDateTime.Minute + ":" + currentDateTime.Second;
    }
}