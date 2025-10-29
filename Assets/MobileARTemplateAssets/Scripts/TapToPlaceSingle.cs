using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARSubsystems;

public class TapToPlaceSingle : MonoBehaviour
{
    [Header("Prefab to place")]
    public GameObject placedPrefab;    
    public GameObject mercuryPrefab;   

    [Header("Placement")]
    [Tooltip("Sun vertical offset above the detected plane (meters).")]
    public float startPosition = 0.5f; 

    [Tooltip("Initial Mercury orbit radius from the Sun (meters).")]
    public float mercuryStartRadius = 0.5f;

    ARRaycastManager raycastMgr;
    ARPlaneManager planeMgr;
    ARAnchorManager anchorMgr;

    GameObject placedObject;   
    GameObject mercuryObject; 
    Transform parentAnchor;  

    static readonly List<ARRaycastHit> hits = new();

    void Awake()
    {
        raycastMgr = GetComponent<ARRaycastManager>();
        planeMgr = GetComponent<ARPlaneManager>();
        anchorMgr = GetComponent<ARAnchorManager>();
        EnhancedTouchSupport.Enable();
    }

    void OnDestroy() => EnhancedTouchSupport.Disable();

    void Update()
    {
        if (!TryGetTap(out var screenPos)) return;
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

            var offset = pose.right * mercuryStartRadius;
            var mercuryPos = sunPos + offset;
            var mercuryRot = Quaternion.LookRotation((sunPos - mercuryPos).normalized, pose.up);

            mercuryObject = Instantiate(mercuryPrefab, mercuryPos, mercuryRot, parentAnchor);

            if (mercuryObject.TryGetComponent<MercuryOrbit>(out var orbit)) orbit.SetOrbitCenter(placedObject.transform);

            SetDistance(mercuryObject, mercuryStartRadius);

            Debug.Log("Spawned Sun + Mercury under parent " + (parentAnchor ? parentAnchor.name : "<none>"));
        }
        else
        {
            placedObject.transform.SetPositionAndRotation(sunPos, sunRot);

            if (mercuryObject != null)
            {
                var offset = pose.right * mercuryStartRadius;
                var mercuryPos = sunPos + offset;
                var mercuryRot = Quaternion.LookRotation((sunPos - mercuryPos).normalized, pose.up);

                mercuryObject.transform.SetPositionAndRotation(mercuryPos, mercuryRot);
            }
        }
    }

    void SetDistance(GameObject other, float dist)
    {
        if (!other || !placedObject) return;

        var dir = (other.transform.position - placedObject.transform.position);
        if (dir.sqrMagnitude < 1e-6f) dir = placedObject.transform.right;
        other.transform.position = placedObject.transform.position + dir.normalized * Mathf.Max(0f, dist);
    }

    bool TryGetTap(out Vector2 pos)
    {
        pos = default;
        var ts = Touchscreen.current;
        if (ts == null) return false;

        var t = ts.primaryTouch;
        if (t.press.wasPressedThisFrame || t.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
        {
            pos = t.position.ReadValue();
            return true;
        }
        return false;
    }
}
