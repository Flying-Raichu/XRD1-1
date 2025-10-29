using UnityEngine;

public class MercuryOrbit : MonoBehaviour
{
    [SerializeField] private Transform orbitCenter;
    [SerializeField] private float degreesPerSecond = 30f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;
    }

    public void SetOrbitCenter(Transform center) => orbitCenter = center;

    void Update()
    {
        if (!orbitCenter) return;

        transform.RotateAround(orbitCenter.position, orbitCenter.up, degreesPerSecond * Time.deltaTime);
    }
}
