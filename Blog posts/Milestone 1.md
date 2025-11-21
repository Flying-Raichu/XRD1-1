# Milestone 1 – AR Setup & Plane Detection
**Author:** Lyubomir & Catarina

## Overview
In this milestone we focused on setting up the core AR Foundation workflow: detecting planes, placing an anchored Sun object, and preparing the scene for the rest of the solar‑system interactions. This part of the project was our introduction to AR tracking, anchors, and how Unity’s AR subsystem manages real‑world geometry.

## Reflection & Theory
Before this project, AR plane detection felt like a “black box.” Working with AR Foundation helped us understand how markerless tracking works: the device continuously collects camera frames + IMU data, detects feature points, and constructs trackables like **ARPlanes**. These trackables act as coordinate systems anchored to the real world, which is crucial for a stable augmented environment.

In terms of theory, this milestone directly connects to:
- **Developing AR applications (Goal 1)**  
- **Understanding tracking, rendering, locomotion & input (Goal 4)**  
- **Understanding underlying sensor technology (Goal 5)**  

We also learned how 6DoF tracking affects object placement. The Sun stays fixed because its transform is parented under an **ARAnchor**, which acts as a persistent reference to real‑world space.

## Practical Implementation
We created a `TapToPlaceSingle` script that listens for a touch on a detected plane and spawns the Sun at that position. The most important part was understanding that ARRaycast hits return positions in real‑world coordinates, not screen space, using the device's camera as a source point.

### Example Code Snippet
```csharp
if (!raycastMgr.Raycast(screenPos, hits, TrackableType.PlaneWithinPolygon))
    return;

var hit = hits[0];
var pose = hit.pose;
var sunPos = pose.position + pose.up * startPosition;
placedObject = Instantiate(placedPrefab, sunPos, pose.rotation, parentAnchor);
```

This snippet shows the core AR interaction:  
1. We raycast onto real‑world planes  
2. We retrieve the **pose** (position + rotation)  
3. We instantiate our Sun prefab at the detected location  

Later on, we also disabled plane detection after the Sun was placed — this helps performance and keeps the UI clean.

```csharp
planeMgr.requestedDetectionMode = PlaneDetectionMode.None;
```

## XR Questions Addressed
**How does markerless tracking work?**  
It detects features in camera images and uses SLAM (Simultaneous Localization and Mapping) to build planes and estimate device movement.

**How does our project add value to the end user?**  
It provides a spatial, tangible view of the solar system, making planetary scale and distance easier to understand.

**How is AR workflow different from desktop Unity?**  
Instead of a fixed coordinate system, AR scenes respond to the physical world. Everything depends on detected surfaces and device tracking.

## References
[1] Unity AR Foundation documentation  
https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@3.0/manual/index.html

[2] Unity tutorial – Placing an object on a plane  
https://learn.unity.com/tutorial/placing-an-object-on-a-plane-in-ar

[3] AR Foundation beginner guide  
https://technerdus.com/unity-ar-foundation-tutorial-for-beginners/
