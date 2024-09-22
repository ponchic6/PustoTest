using System;
using UnityEngine;

public class ArrowsMover : MonoBehaviour
{
    [SerializeField] private Ticker ticker;
    [SerializeField] private Transform secondArrow;
    [SerializeField] private Transform minuteArrow;
    [SerializeField] private Transform hourArrow;

    private void Awake()
    {
        ticker.OnTimeSecondTick += MoveSecondArrow;
        ticker.OnTimeSet += SetTime;
    }

    private void OnDisable()
    {
        ticker.OnTimeSecondTick -= MoveSecondArrow;
        ticker.OnTimeSet -= SetTime;
    }

    private void SetTime(DateTime dateTime)
    {
        secondArrow.rotation = Quaternion.identity;
        minuteArrow.rotation = Quaternion.identity;
        hourArrow.rotation = Quaternion.identity;
        MoveHourArrow(dateTime.Hour);
        MoveMinuteArrow(dateTime.Minute);
        MoveSecondArrow(dateTime.Second);
    }

    private void MoveSecondArrow(float seconds)
    {
        secondArrow.Rotate(-secondArrow.forward, seconds * 6);
        minuteArrow.Rotate(-secondArrow.forward, seconds * (1 / 10f));
        hourArrow.Rotate(-secondArrow.forward, seconds * (1 / 120f));
    }

    private void MoveMinuteArrow(float minutes)
    {
        secondArrow.Rotate(-secondArrow.forward, minutes * 360);
        minuteArrow.Rotate(-secondArrow.forward, minutes * 6);
        hourArrow.Rotate(-secondArrow.forward, minutes * 0.5f);
    }

    private void MoveHourArrow(float hours)
    {
        secondArrow.Rotate(-secondArrow.forward, hours * 3600 * 6);
        minuteArrow.Rotate(-secondArrow.forward, hours * 360);
        hourArrow.Rotate(-secondArrow.forward, hours * 30);
    }
}