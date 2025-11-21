# Milestone 4 — Planet Info UI & Educational Content  
**Authors:** Lyubomir

## Overview

By this milestone, our solar system was moving and interactive, but it still wasn’t clearly **educational**. You could see the planets and select them, but you couldn’t really *learn* anything from that. The goal for Milestone 4 was to connect the interaction we already had with proper UI and text content, so the app behaves like a small AR learning tool instead of just a simulation.

We focused on three main things here:

- A simple data component for storing planet information  
- A reusable info panel UI that can be opened from any planet  
- Wiring up the interaction so tapping a label shows the correct content  

This pushed us to think about UI design, information architecture, and how to integrate 2D interfaces into a 3D AR scene.

---

## Planet Data with `PlanetInfo`

We started by creating a lightweight component to hold per-planet data:

```csharp
public class PlanetInfo : MonoBehaviour
{
    [Header("Display")]
    public string displayName;

    [TextArea]
    public string description;

    [Tooltip("Optional icon shown in the info panel.")]
    public Sprite icon;
}
```

Each planet prefab now has:

- A **display name** (e.g. “Mars”)  
- A **description** with a short explanation and a couple of fun facts  
- An optional **icon** sprite for visuals  

This gave us a clean separation between “what the planet is” (data) and “how we show it” (UI).

---

## Reusable Info Panel with `PlanetInfoPanel`

Next, we built a UI panel to present this information. The panel lives on a screen-space canvas and is managed by the `PlanetInfoPanel` singleton:

```csharp
public class PlanetInfoPanel : MonoBehaviour
{
    public static PlanetInfoPanel Instance { get; private set; }

    [Header("References")]
    public GameObject root;
    public TMP_Text titleText;
    public TMP_Text bodyText;
    public Image iconImage;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        if (root == null)
            root = gameObject;

        Hide();
    }

    public void Show(PlanetInfo info)
    {
        if (info == null) return;
        root.SetActive(true);

        titleText.text = string.IsNullOrEmpty(info.displayName) ? info.name : info.displayName;
        bodyText.text = info.description;

        if (iconImage != null)
        {
            if (info.icon != null)
            {
                iconImage.sprite = info.icon;
                iconImage.enabled = true;
            }
            else
            {
                iconImage.enabled = false;
            }
        }
    }

    public void Hide()
    {
        if (root != null)
            root.SetActive(false);
    }

    public void OnCloseButtonClicked()
    {
        Hide();
    }
}
```

The singleton pattern here is simple but practical: we only ever want one info panel in the scene, and any planet can ask it to show itself.

---

## Connecting AR Interaction to UI: `PlanetLabelClick`

To trigger the info panel, we reused the world-space labels that appear when a planet is highlighted. We added a `PlanetLabelClick` script that implements `IPointerClickHandler`:

```csharp
public class PlanetLabelClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        var info = GetComponentInParent<PlanetInfo>();
        if (info != null && PlanetInfoPanel.Instance != null)
        {
            Debug.Log("Planet label was clicked: " + info.displayName);
            PlanetInfoPanel.Instance.Show(info);
        }
        else if (info == null)
        {
            Debug.Log("Planet label clicked but info missing.");
        }
        else
        {
            Debug.Log("Planet label clicked but panel missing.");
        }
    }
}
```

This ties everything together:

1. The user taps a planet → it gets highlighted  
2. The label appears above it  
3. Tapping the label opens the info panel with the correct planet’s data  

One XR-specific challenge we had to keep in mind was **input priority**: AR taps used for plane placement should not override UI clicks. We solved this earlier in `TapToPlaceSingle` using `EventSystem.RaycastAll`, and that work paid off here as well—the UI panel now opens reliably without accidentally moving the whole solar system.

---

## XR Reflection & Study Goals

This milestone is where the app finally feels like an **educational AR experience** rather than just a demo. It addresses several course goals:

- **Goal 1 – Developing XR applications:** we combined AR tracking, 3D rendering, and 2D UI into one coherent experience.  
- **Goal 3 – Understanding XR technologies and use cases:** we used AR to support conceptual learning (planet properties, scale, orbits) instead of just visual spectacle.  
- **Goal 6 – Conceiving XR-based experiences:** we designed the interaction flow deliberately: select → label → info panel.

We also learned about practical details like:

- The difference between world-space and screen-space UI  
- How event systems route pointer clicks  
- How to expose data safely through components rather than hard-coding text in scripts  

Overall, this milestone turned our solar system into something a teacher could actually use to introduce students to basic astronomy concepts.

---

## References

[1] Unity Technologies. “TextMeshPro Essentials.” *Unity Documentation*.  
https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest  

[2] Unity Technologies. “EventSystem and UI Input.” *Unity Manual*.  
https://docs.unity3d.com/Manual/EventSystem.html  

[3] Unity Technologies. “IPointerClickHandler Interface.” *Unity Scripting API*.  
https://docs.unity3d.com/ScriptReference/EventSystems.IPointerClickHandler.html  

[4] Unity Technologies. “Canvas and UI Rendering.” *Unity Manual*.  
https://docs.unity3d.com/Manual/class-Canvas.html  
