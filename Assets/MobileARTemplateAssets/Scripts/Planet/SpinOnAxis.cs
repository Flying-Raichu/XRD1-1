using UnityEngine;

public class SpinOnAxis : MonoBehaviour
{
    [Header("Target to spin (optional)")]
    [Tooltip("Leave empty to spin this transform.")]
    public Transform target;

    [Header("Spin Settings")]
    public float degreesPerSecond = 30f;
    public float axialTiltDegrees = 0f;

    void Awake()
    {
        if (target == null)
            target = transform;

        if (!Mathf.Approximately(axialTiltDegrees, 0f))
        {
            target.localRotation = Quaternion.Euler(axialTiltDegrees, 0f, 0f) * target.localRotation;
        }
    }

    void Update()
    {
        if (target == null) return;

        float scale = SolarSystemTime.TimeScale;
        if (Mathf.Approximately(scale, 0f)) return;

        float angle = degreesPerSecond * scale * Time.deltaTime;
        target.Rotate(Vector3.up, angle, Space.Self);
    }
}
