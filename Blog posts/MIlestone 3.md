# Milestone 3 — Planet Interaction, Highlighting & Selection  
**Author:** Lyubomir

## Overview

After getting the planets to spawn and orbit correctly, this milestone was about making the system **interactive**. Instead of just watching planets move, we wanted the user to be able to tap them, see which one is selected, and start exposing information in a clear way. Concretely, this meant implementing:

- A planet selection system that works well in AR  
- Visual highlighting using material emission  
- World-space labels that always face the camera  

This work ties directly into the study goals about **user interaction in XR**, **input handling**, and designing **usable educational AR experiences**.

---

## Practical Development Work

### 1. Screen-Space Selection with `PlanetSelectionManager`

Raycasting against tiny planet colliders in AR turned out to be unreliable, so we designed a custom selection system in `PlanetSelectionManager`:

```csharp
public bool TrySelectPlanet(Vector2 screenPos)
{
    if (arCam == null || allPlanets == null || allPlanets.Length == 0)
        return false;

    PlanetHighlight best = null;
    float bestDist = float.MaxValue;

    foreach (var planet in allPlanets)
    {
        if (planet == null) continue;

        Vector3 planetScreenPos = arCam.WorldToScreenPoint(planet.transform.position);
        if (planetScreenPos.z < 0f) continue;

        float dist = Vector2.Distance(screenPos, planetScreenPos);

        if (dist < tapRadiusPx && dist < bestDist)
        {
            bestDist = dist;
            best = planet;
        }
    }

    if (best != null)
    {
        if (current != null)
            current.Unhighlight();

        current = best;
        current.Highlight();
        Debug.Log("Selected planet: " + best.name);
        return true;
    }

    return false;
}
```

Instead of relying on physics, we convert each planet’s world position to **screen space** and choose the closest one to the touch within a configurable radius. This approach is much more forgiving on mobile and matches how many real apps implement tapping small objects.

From an XR perspective, this made us think about **how input coordinates transform** between device space, screen space, and world space.

---

### 2. Visual Feedback with `PlanetHighlight`

Once a planet is selected, we wanted clear visual feedback. The `PlanetHighlight` script handles this by boosting the emission color of the planet’s material and toggling the label:

```csharp
public void Highlight()
{
    if (hasEmission)
    {
        Color newColor = originalEmission + Color.white * emissionStrength;
        planetMat.SetColor("_EmissionColor", newColor);
        planetMat.EnableKeyword("_EMISSION");
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
```

---

### 3. World-Space Labels & Billboarding

Each planet has a world-space label (a small Canvas with text) that appears when highlighted. To keep it readable from any angle, we used a simple billboarding script:

```csharp
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
```

This ensures the label always faces the AR camera, which is especially important in AR because the user can physically move around the system.

We also added `WorldSpaceCanvasCamera` to automatically assign the AR camera to world-space canvases, avoiding the “UI is invisible because there is no camera assigned” issue.

---

## XR Reflection & Study Goals

**How does this contribute to the user experience?**  
Instead of passively viewing the system, the user can now **select** planets and clearly see which one is active, with readable labels that stay oriented correctly. This is a big step toward the educational goals of the app.

**What XR concepts did we practice?**  
- Converting between world and screen coordinates  
- Designing clear visual feedback in AR  
- Understanding how world-space UI works and interacts with the AR camera  
- Managing input so selection doesn’t fight with placement

This milestone deepened our understanding of **input handling** and **interaction design** within an AR context, which directly supports the course outcomes related to XR interaction and usability.

---

## References

[1] Unity Technologies. “World-Space UI.” *Unity Manual*.  
https://docs.unity3d.com/Manual/HOWTO-UIWorldSpace.html  

[2] Unity Technologies. “Camera.WorldToScreenPoint.” *Unity Scripting API*.  
https://docs.unity3d.com/ScriptReference/Camera.WorldToScreenPoint.html  

[3] Unity Technologies. “Material Emission Properties.” *Unity Manual*.  
https://docs.unity3d.com/Manual/StandardShaderMaterialParameterEmission.html  

[4] Unity Technologies. “Event System & Input in Unity UI.” *Unity Manual*.  
https://docs.unity3d.com/Manual/EventSystem.html  
