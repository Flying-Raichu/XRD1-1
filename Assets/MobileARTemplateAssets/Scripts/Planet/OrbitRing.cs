using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitRing : MonoBehaviour
{
    LineRenderer lr;

    float radius;
    int segments;
    Vector3 normal;
    Material mat;

    public void Build(
        float radius,
        int segments,
        float lineWidth,
        Vector3 center,
        Vector3 normal,
        Material mat)
    {
        lr = GetComponent<LineRenderer>();
        lr.loop = true;
        lr.useWorldSpace = true;

        lr.widthMultiplier = lineWidth;
        lr.material = mat;

        this.radius = radius;
        this.segments = segments;
        this.normal = normal.normalized;
        this.mat = mat;

        lr.positionCount = segments;
        UpdateRing(center);
    }

    public void UpdateRing(Vector3 center)
    {
        if (!lr) return;

        Vector3 tangent = Vector3.Cross(normal, Vector3.up);
        if (tangent.sqrMagnitude < 1e-4f)
            tangent = Vector3.Cross(normal, Vector3.right);
        tangent.Normalize();

        Vector3 bitangent = Vector3.Cross(normal, tangent);

        for (int i = 0; i < segments; i++)
        {
            float t = (float)i / segments * Mathf.PI * 2f;
            float cos = Mathf.Cos(t);
            float sin = Mathf.Sin(t);

            Vector3 point =
                center +
                (tangent * cos + bitangent * sin) * radius;

            lr.SetPosition(i, point);
        }
    }
}
