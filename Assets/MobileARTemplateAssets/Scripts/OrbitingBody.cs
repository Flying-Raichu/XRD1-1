using UnityEngine;

public class OrbitingBody : MonoBehaviour
{
    Transform orbitCenter;
    float degreesPerSecond;
    Vector3 orbitAxis = Vector3.up;

    public void Init(Transform center, float speedDeg, Vector3 axis)
    {
        orbitCenter = center;
        degreesPerSecond = speedDeg;
        orbitAxis = axis.normalized;
    }

    void Update()
    {
        if (!orbitCenter) return;
        transform.RotateAround(orbitCenter.position, orbitAxis, degreesPerSecond * Time.deltaTime);
    }
}
