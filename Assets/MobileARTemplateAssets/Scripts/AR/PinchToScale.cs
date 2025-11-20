using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PinchToScale : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Root transform of the solar system that should scale (anchor or Sun).")]
    public Transform target;

    [Header("Limits")]
    [Range(0.05f, 5f)]
    public float minScale = 0.2f;

    [Range(0.05f, 5f)]
    public float maxScale = 2.5f;

    [Header("Sensitivity")]
    [Tooltip("How fast the scale changes per pinch pixel delta.")]
    public float sensitivity = 0.002f;

    float lastDistance;
    bool pinching;

    void Update()
    {
        if (target == null)
            return;

        var touches = Touch.activeTouches;
        if (touches.Count < 2)
        {
            pinching = false;
            return;
        }

        var t0 = touches[0];
        var t1 = touches[1];

        if (t0.phase == UnityEngine.InputSystem.TouchPhase.Ended ||
            t0.phase == UnityEngine.InputSystem.TouchPhase.Canceled ||
            t1.phase == UnityEngine.InputSystem.TouchPhase.Ended ||
            t1.phase == UnityEngine.InputSystem.TouchPhase.Canceled)
        {
            pinching = false;
            return;
        }

        float currentDistance = Vector2.Distance(t0.screenPosition, t1.screenPosition);

        if (!pinching)
        {
            pinching = true;
            lastDistance = currentDistance;
            return;
        }

        float delta = currentDistance - lastDistance;
        lastDistance = currentDistance;

        if (Mathf.Approximately(delta, 0f))
            return;

        float scaleFactor = 1f + delta * sensitivity;

        float current = target.localScale.x;
        current *= scaleFactor;
        current = Mathf.Clamp(current, minScale, maxScale);

        target.localScale = Vector3.one * current;
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }
}
