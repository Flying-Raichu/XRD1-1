using UnityEngine;

public class OrbitingBody : MonoBehaviour
{
    [SerializeField] private Transform orbitCenter;
    [SerializeField] private float degreesPerSecond = 30f;
    [SerializeField] private Vector3 orbitAxis = Vector3.up;

    public void Init(Transform center, float speed, Vector3 axis)
    {
        orbitCenter = center;
        degreesPerSecond = speed;
        orbitAxis = axis.normalized;
    }

    void Update()
    {
        if (!orbitCenter) return;

        float scale = SolarSystemTime.TimeScale;
        if (Mathf.Approximately(scale, 0f)) return;

        float angle = degreesPerSecond * scale * Time.deltaTime;
        transform.RotateAround(orbitCenter.position, orbitAxis, angle);
    }
}
