using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    [Header("Display")]
    public string displayName;

    [TextArea]
    public string description;

    [Tooltip("Optional icon shown in the info panel.")]
    public Sprite icon;
}
