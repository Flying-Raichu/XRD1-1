using UnityEngine;

public class PlanetRing : MonoBehaviour
{
    [Header("Ring shape")]
    [Range(3, 360)]
    public int segments = 64;

    [Tooltip("Inner radius of the ring (distance from planet center).")]
    public float innerRadius = 0.7f;

    [Tooltip("How wide the ring is (outerRadius = innerRadius + thickness).")]
    public float thickness = 0.5f;

    [Header("Appearance")]
    [Tooltip("Material that uses the Saturn ring strip texture.")]
    public Material ringMat;

    GameObject ringObject;
    Mesh ringMesh;
    MeshFilter ringMF;
    MeshRenderer ringMR;

    void Start()
    {
        ringObject = new GameObject(name + " Ring");
        ringObject.transform.SetParent(transform, false); 
        ringObject.transform.localPosition = Vector3.zero;
        ringObject.transform.localRotation = Quaternion.identity;
        ringObject.transform.localScale = Vector3.one;

        ringMF = ringObject.AddComponent<MeshFilter>();
        ringMR = ringObject.AddComponent<MeshRenderer>();

        ringMesh = new Mesh();
        ringMF.mesh = ringMesh;

        ringMR.material = ringMat;

        BuildRingMesh();
    }

    void BuildRingMesh()
    {
        int vertexCount = (segments + 1) * 2;

        Vector3[] vertices = new Vector3[vertexCount];
        Vector2[] uv = new Vector2[vertexCount];

        int[] triangles = new int[segments * 6];

        int vert = 0;
        int tri = 0;

        float outerRadius = innerRadius + thickness;

        for (int i = 0; i <= segments; i++)
        {
            float progress = (float)i / segments;       
            float angle = progress * Mathf.PI * 2f;      

            float x = Mathf.Sin(angle);
            float z = Mathf.Cos(angle);

            Vector3 outerPos = new Vector3(x, 0f, z) * outerRadius;
            Vector3 innerPos = new Vector3(x, 0f, z) * innerRadius;

            vertices[vert] = outerPos;
            vertices[vert + 1] = innerPos;


            uv[vert] = new Vector2(0f, 0f);
            uv[vert + 1] = new Vector2(1f, 0f);

            if (i < segments)
            {
                triangles[tri] = vert;
                triangles[tri + 1] = vert + 2;
                triangles[tri + 2] = vert + 1;

                triangles[tri + 3] = vert + 1;
                triangles[tri + 4] = vert + 2;
                triangles[tri + 5] = vert + 3;

                tri += 6;
            }

            vert += 2;
        }

        ringMesh.Clear();
        ringMesh.vertices = vertices;
        ringMesh.uv = uv;
        ringMesh.triangles = triangles;
        ringMesh.RecalculateNormals();
    }
}
