using UnityEngine;

public class ArrowHandler : MonoBehaviour
{
    private const float Eps = 0.01f;

    [SerializeField] private Ticker ticker;
    private Camera _mainCamera;

    private void Awake() => _mainCamera = Camera.main;

    private void OnMouseDrag()
    {
        if (ticker.CanTick)
        {
            ticker.CanTick = false;
        }
        
        var parent = transform.parent;
        var difference = CalculateRotationDifference(parent);
        var delta = -difference.eulerAngles.z;
        
        if (Mathf.Abs(delta) > Eps)
        {
            ticker.AddSeconds(delta * 120);
        }
    }

    private Quaternion CalculateRotationDifference(Transform parent)
    {
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = mousePosition - parent.position;
        directionToMouse.z = 0;
        var angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        return Quaternion.Inverse(parent.rotation) * Quaternion.Euler(0, 0, angle - 90);
    }
}