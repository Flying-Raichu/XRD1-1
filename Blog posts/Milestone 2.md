# Milestone 2 — Planet Spawning, Orbits & Rotation  
**Authors:** Catarina & Lyubomir

## Overview

In this milestone we moved from “a single Sun in AR” to an actual **mini solar system**. The goal was to spawn all planets around the Sun, give them orbits, and make the whole system react correctly to how the user places it in the real world. Technically, this meant combining AR plane placement from Milestone 1 with our own orbit logic, rotation scripts, and some basic data-driven design.

This work ties directly into the study goals about **developing AR applications**, understanding **tracking and rendering**, and learning how to build **interactive XR experiences** that are more than just static 3D models.

---

## Practical Development Work

### 1. Data-Driven Planet Setup

Instead of hard‑coding each planet, we added a small serializable class **inside** `TapToPlaceSingle`:

```csharp
[System.Serializable]
public class PlanetSpec
{
    public string name;
    public GameObject prefab;

    [Tooltip("Orbit radius from the Sun (meters).")]
    public float orbitRadius = 0.5f;

    [Tooltip("Orbit speed (degrees per second).")]
    public float degreesPerSecond = 30f;

    [Tooltip("Starting angle around the Sun (degrees).")]
    public float startingAngleDeg = 0f;
}
```

The script then exposes a list:

```csharp
[Header("Planets")]
public List<PlanetSpec> planets = new();
```

This let us configure all planets straight from the Inspector (prefab, orbit radius, speed, starting angle) without changing code every time.

---

### 2. Spawning Planets Around the Sun

Once the user taps a plane and the Sun is placed, we call `SpawnPlanetsAndRings`. For each `PlanetSpec` we:

1. Compute a direction on the AR plane from the Sun  
2. Multiply by the orbit radius  
3. Instantiate the planet at that position  

```csharp
float angleRad = spec.startingAngleDeg * Mathf.Deg2Rad;

Vector3 dirInPlane =
    Mathf.Cos(angleRad) * planePose.right +
    Mathf.Sin(angleRad) * planePose.forward;

dirInPlane.Normalize();

Vector3 planetPos = sunPos + dirInPlane * spec.orbitRadius;

Quaternion planetRot =
    Quaternion.LookRotation((sunPos - planetPos).normalized, planePose.up);

var planetObj = Instantiate(spec.prefab, planetPos, planetRot, root);
```

This code is heavily dependent on AR concepts: `planePose.right` and `planePose.forward` come from AR Foundation and give us the local axes of the detected plane. That’s how we ensure the system is aligned with the real-world surface the user tapped, regardless of device orientation.

---

### 3. Orbits with `OrbitingBody`

To make the planets move, we created a reusable `OrbitingBody` component:

```csharp
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
```

From `TapToPlaceSingle` we call:

```csharp
if (planetObj.TryGetComponent<OrbitingBody>(out var orbit))
{
    orbit.Init(placedObject.transform, spec.degreesPerSecond, orbitAxis);
}
```

This is where a lot of our XR understanding clicked: instead of moving planets manually, we let Unity handle rotation around a point in 3D space (`RotateAround`). The Sun’s transform becomes the reference frame for all orbits.

---

### 4. Visualizing Orbits with `OrbitRing`

To make the system easier to read, we added **orbit rings** using a `LineRenderer` inside the `OrbitRing` script:

```csharp
[RequireComponent(typeof(LineRenderer))]
public class OrbitRing : MonoBehaviour
{
    public void Build(
        float radius,
        int segments,
        float lineWidth,
        Vector3 center,
        Vector3 normal,
        Material mat)
    {
        // configure LineRenderer and generate a circular ring
        // around 'center' in the plane defined by 'normal'
    }
}
```

Each ring is parented under the same root (either the AR anchor or the Sun), so it stays aligned with the orbital motion and the AR plane.

---

## XR Concepts & Reflection

**How does this connect to tracking and rendering?**  
All positions are ultimately relative to AR trackables (planes and anchors). The planets only look stable because we attach them to an `ARAnchor`, which is continuously updated based on sensor data.

**How does this benefit the user?**  
Instead of an abstract, flat diagram, the user gets a *spatial* sense of the solar system—seeing planets at different distances and speeds, anchored into their own room.

**What did we learn about XR development?**  
- Transforms in AR are always relative to detected surfaces  
- Small mathematical mistakes (like mixing up axes) produce huge visual errors  
- Good AR design means thinking about user placement, not just scene coordinates

This milestone was where our project started feeling like a real AR experience instead of just spawning meshes in space.

---

## References

[1] Unity Technologies. “AR Foundation Manual.” *Unity Documentation*.  
https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@5.0/manual/index.html  

[2] Unity Technologies. “ARRaycastManager.” *Unity Scripting API*.  
https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@5.0/api/UnityEngine.XR.ARFoundation.ARRaycastManager.html  

[3] Unity Technologies. “Transform.RotateAround.” *Unity Scripting API*.  
https://docs.unity3d.com/ScriptReference/Transform.RotateAround.html  

[4] Unity Technologies. “LineRenderer Component.” *Unity Manual*.  
https://docs.unity3d.com/Manual/class-LineRenderer.html  

[5] Boards to bits games. "Making a Custom Planet Ring in Unity".

https://www.youtube.com/watch?v=Rze4GEFrYYs&t=1s
