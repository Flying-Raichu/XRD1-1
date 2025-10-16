using UnityEngine;

public class MercuryOrbit : MonoBehaviour
{
    [SerializeField] private GameObject orbitCenter;  
    [SerializeField] private GameObject mercury;      
    private float speed = 3.0f;                       

    void Update()
    {
        OrbitingAroundSun(mercury); 
    }

    void OrbitingAroundSun(GameObject objectToOrbit)
    {
       
        Vector3 orbitAxis = Vector3.up;  
        transform.RotateAround(orbitCenter.transform.position, orbitAxis, speed * Time.deltaTime);
    }
}
