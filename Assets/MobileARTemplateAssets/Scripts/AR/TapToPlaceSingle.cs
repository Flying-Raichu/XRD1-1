using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.EventSystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TapToPlaceSingle : MonoBehaviour
{
    [Header("Sun")]
    [Tooltip("Prefab for the Sun object.")]
    public GameObject placedPrefab;

    [Tooltip("Sun vertical offset above the detected plane (meters).")]
    public float startPosition = 0.5f;

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

    [Header("Planets")]
    public List<PlanetSpec> planets = new();

    [Header("Orbit visuals")]
    public Material orbitMaterial;
    public int orbitSegments = 90;
    public float orbitLineWidth = 0.003f;

    [Header("Interaction")]
    [Tooltip("Handles tapping planets so placement taps don't override selection.")]
    public PlanetSelectionManager selectionManager;

    ARRaycastManager raycastMgr;
    ARPlaneManager planeMgr;
    ARAnchorManager anchorMgr;

    GameObject placedObject;
    Transform parentAnchor;

    readonly List<GameObject> spawnedPlanets = new();
    readonly List<OrbitRing> spawnedRings = new();

    static readonly List<ARRaycastHit> hits = new();

    void Awake()
    {
        raycastMgr = GetComponent<ARRaycastManager>();
        planeMgr = GetComponent<ARPlaneManager>();
        anchorMgr = GetComponent<ARAnchorManager>();

        EnhancedTouchSupport.Enable();

        if (selectionManager == null)
            selectionManager = FindFirstObjectByType<PlanetSelectionManager>();
    }

    void OnDestroy()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        if (!TryGetTap(out var screenPos)) return;

        if (IsPointerOverUI(screenPos))
            return;

        if (selectionManager != null && selectionManager.TrySelectPlanet(screenPos))
            return;

        if (!raycastMgr.Raycast(screenPos, hits, TrackableType.PlaneWithinPolygon)) return;

        var hit = hits[0];
        var pose = hit.pose;

        var sunPos = pose.position + pose.up * startPosition;
        var sunRot = pose.rotation;

        if (parentAnchor == null)
        {
            Transform parent = null;

            if (anchorMgr != null && planeMgr != null)
            {
                var plane = planeMgr.GetPlane(hit.trackableId);
                var anchor = plane ? anchorMgr.AttachAnchor(plane, pose) : null;
                if (anchor != null) parent = anchor.transform;
            }

            parentAnchor = parent;
        }

        if (placedObject == null)
        {
            placedObject = Instantiate(placedPrefab, sunPos, sunRot, parentAnchor);

            SpawnPlanetsAndRings(pose, sunPos);

            var scaler = FindFirstObjectByType<PinchToScale>();
            if (scaler != null)
            {
                var root = parentAnchor != null ? parentAnchor : placedObject.transform;
                scaler.SetTarget(root);
            }

            if (planeMgr != null)
            {
                planeMgr.requestedDetectionMode = PlaneDetectionMode.None;
            }

            Debug.Log("Spawned Sun + planets under parent " +
                      (parentAnchor ? parentAnchor.name : "<none>"));
        }
        else
        {
            Vector3 delta = sunPos - placedObject.transform.position;

            if (parentAnchor != null)
            {
                parentAnchor.position += delta;
                parentAnchor.rotation = sunRot;
            }
            else
            {
                placedObject.transform.position += delta;
                placedObject.transform.rotation = sunRot;

                foreach (var p in spawnedPlanets)
                {
                    if (p != null)
                        p.transform.position += delta;
                }
            }

            foreach (var ring in spawnedRings)
            {
                if (ring != null)
                    ring.UpdateRing(sunPos);
            }
        }
    }

    void SpawnPlanetsAndRings(Pose planePose, Vector3 sunPos)
    {
        Vector3 orbitAxis = planePose.up;

        spawnedPlanets.Clear();
        spawnedRings.Clear();

        Transform root = parentAnchor != null ? parentAnchor : placedObject.transform;

        foreach (var spec in planets)
        {
            if (spec.prefab == null)
                continue;

            if (orbitMaterial != null && spec.orbitRadius > 0f)
            {
                var ringObj = new GameObject($"{spec.name} Orbit");
                ringObj.transform.SetParent(root, false);

                var ring = ringObj.AddComponent<OrbitRing>();
                ring.Build(
                    spec.orbitRadius,
                    orbitSegments,
                    orbitLineWidth,
                    sunPos,
                    orbitAxis,
                    orbitMaterial);

                spawnedRings.Add(ring);
            }

            float angleRad = spec.startingAngleDeg * Mathf.Deg2Rad;

            Vector3 dirInPlane =
                Mathf.Cos(angleRad) * planePose.right +
                Mathf.Sin(angleRad) * planePose.forward;

            dirInPlane.Normalize();

            Vector3 planetPos = sunPos + dirInPlane * spec.orbitRadius;

            Quaternion planetRot =
                Quaternion.LookRotation((sunPos - planetPos).normalized, planePose.up);

            var planetObj = Instantiate(spec.prefab, planetPos, planetRot, root);

            if (planetObj.TryGetComponent<OrbitingBody>(out var orbit))
            {
                orbit.Init(placedObject.transform, spec.degreesPerSecond, orbitAxis);
            }

            spawnedPlanets.Add(planetObj);
        }

        if (selectionManager != null)
        {
            selectionManager.RefreshPlanets();
        }
    }


    bool TryGetTap(out Vector2 pos)
    {
        pos = default;

        if (Touch.activeTouches.Count != 1)
            return false;

        var ts = Touchscreen.current;
        if (ts == null) return false;

        var t = ts.primaryTouch;

        if (t.press.wasPressedThisFrame ||
            t.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
        {
            pos = t.position.ReadValue();
            return true;
        }

        return false;
    }


    bool IsPointerOverUI(Vector2 screenPos)
    {
        if (EventSystem.current == null)
            return false;

        var eventData = new PointerEventData(EventSystem.current)
        {
            position = screenPos
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
