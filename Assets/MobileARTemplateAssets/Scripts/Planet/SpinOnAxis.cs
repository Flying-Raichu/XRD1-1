using UnityEngine;

public class SpinOnAxis : MonoBehaviour
{
    [Header("Target to spin")]
    [Tooltip("Leave empty to spin this transform. Assign a child if you want tilt without affecting orbit.")]
    [SerializeField] private Transform target;

    [Header("Rotation")]
    [Tooltip("Degrees the planet rotates around its axis per second (positive = prograde, negative = retrograde).")]
    [SerializeField] private float degreesPerSecond = 10f;

    [Tooltip("Axial tilt in degrees (e.g. Earth ~23.5).")]
    [SerializeField] private float axialTiltDegrees = 0f;

    void Awake()
    {
        if (target == null)
            target = transform;

        target.localRotation = Quaternion.Euler(axialTiltDegrees, 0f, 0f) * target.localRotation;
    }

    void Update()
    {
        if (Mathf.Approximately(degreesPerSecond, 0f)) return;

        target.Rotate(Vector3.up, degreesPerSecond * Time.deltaTime, Space.Self);
    }
}
