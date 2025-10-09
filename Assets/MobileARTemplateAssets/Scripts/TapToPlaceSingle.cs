using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class TapToPlaceSingle : MonoBehaviour
{
    [Header("Prefab to place")]
    public GameObject placedPrefab;

    ARRaycastManager raycastMgr;
    ARPlaneManager planeMgr;
    ARAnchorManager anchorMgr;

    float startPosition = 0.5f;

    GameObject placedObject;
    static readonly List<ARRaycastHit> hits = new();

    void Awake()
    {
        raycastMgr = GetComponent<ARRaycastManager>();
        planeMgr = GetComponent<ARPlaneManager>();
        anchorMgr = GetComponent<ARAnchorManager>();
        EnhancedTouchSupport.Enable();
    }

    void OnDestroy()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        if (!TryGetTap(out var screenPos)) return;

        if (!raycastMgr.Raycast(screenPos, hits, TrackableType.PlaneWithinPolygon))
            return;

        var hit = hits[0];
        var pose = hit.pose;
        var adjustedPosition = pose.position + pose.up*startPosition;

        if (placedObject == null)
        {
            Transform parent = null;
            if (anchorMgr != null && planeMgr != null)
            {
                var plane = planeMgr.GetPlane(hit.trackableId);
                var anchor = plane ? anchorMgr.AttachAnchor(plane, pose) : null;
                if (anchor != null) parent = anchor.transform;
            }

            placedObject = parent != null
                ? Instantiate(placedPrefab, parent)
                : Instantiate(placedPrefab, adjustedPosition, pose.rotation);
        }
        else
        {
            placedObject.transform.SetPositionAndRotation(adjustedPosition, pose.rotation);
        }
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