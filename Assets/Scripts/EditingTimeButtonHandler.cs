using UnityEngine;
using UnityEngine.UI;

public class EditingTimeButtonHandler : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Ticker ticker;

    private void Awake() => button.onClick.AddListener(StartTime);
    private void OnDisable() => button.onClick.RemoveListener(StartTime);
    private void StartTime() => ticker.CanTick = true;
}