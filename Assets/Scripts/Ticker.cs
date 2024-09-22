using System;
using UnityEngine;

public class Ticker : MonoBehaviour
{
    public event Action<float> OnTimeSecondTick;
    public event Action<DateTime> OnTimeSet;
    
    [SerializeField] private TimeWebRequester timeWebRequester;

    public bool CanTick { get; set; }

    private void Awake() => timeWebRequester.OnTimeSynchronization += SynchronizeTime;
    private void OnDisable() => timeWebRequester.OnTimeSynchronization -= SynchronizeTime;
    private void Update()
    {
        if (CanTick)
        {
            AddSeconds(Time.deltaTime);
        }
    }

    private void SynchronizeTime(long ticks)
    {
        CanTick = true;
        DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(ticks).DateTime;
        OnTimeSet?.Invoke(dateTime);
    }

    public void AddSeconds(float deltaSeconds) => OnTimeSecondTick?.Invoke(deltaSeconds);

    public void SynchronizeTime(DateTime dateTime)
    {
        CanTick = true;
        OnTimeSet?.Invoke(dateTime);
    }
}