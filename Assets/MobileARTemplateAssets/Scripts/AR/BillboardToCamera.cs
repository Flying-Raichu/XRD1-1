using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    private Camera cam;

    void LateUpdate()
    {
        if (cam == null)
            cam = Camera.main;

        if (cam == null) return;

        var t = transform;
        t.rotation = Quaternion.LookRotation(
            t.position - cam.transform.position,
            cam.transform.up);
    }
}
