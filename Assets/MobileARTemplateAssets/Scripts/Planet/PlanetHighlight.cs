using UnityEngine;
using UnityEngine.Rendering;

public class PlanetHighlight : MonoBehaviour
{
    [Header("Highlight Strength")]
    [Range(0f, 10f)]
    public float emissionStrength = 3f;

    [Header("Label (optional)")]
    [Tooltip("World-space label root (Canvas) to show when highlighted.")]
    public GameObject labelRoot;

    private Renderer mr;
    private Material planetMat;
    private Color originalEmission;
    private bool hasEmission;

    void Awake()
    {
        mr = GetComponent<Renderer>();
        if (!mr)
            mr = GetComponentInChildren<Renderer>();

        planetMat = mr.material;

        hasEmission = planetMat.HasProperty("_EmissionColor");
        if (hasEmission)
        {
            originalEmission = planetMat.GetColor("_EmissionColor");

            planetMat.EnableKeyword("_EMISSION");
            planetMat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;

            Debug.Log($"[{name}] Original emission: {originalEmission}");
        }
        else
        {
            Debug.LogWarning($"[{name}] Material {planetMat.name} has no _EmissionColor property");
        }

        if (labelRoot != null)
            labelRoot.SetActive(false);
    }

    public void Highlight()
    {
        if (hasEmission)
        {
            Color newColor = originalEmission + Color.white * emissionStrength;
            planetMat.SetColor("_EmissionColor", newColor);
            planetMat.EnableKeyword("_EMISSION");

            Debug.Log($"[{name}] Highlight emission: {newColor}");
        }

        if (labelRoot != null)
            labelRoot.SetActive(true);
    }

    public void Unhighlight()
    {
        if (hasEmission)
        {
            planetMat.SetColor("_EmissionColor", originalEmission);
            planetMat.EnableKeyword("_EMISSION");
        }

        if (labelRoot != null)
            labelRoot.SetActive(false);
    }
}
