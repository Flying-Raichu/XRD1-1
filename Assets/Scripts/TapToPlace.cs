using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;

public class TapToPlace : MonoBehaviour
{
    public void RemoveAllAnchors()
    {
        foreach (var anchor in m_ReferencePoint)
        {
            m_ReferencePointManager.TryRemoveAnchor(anchor);
        }
        m_ReferencePoint.Clear();
    }


    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_ReferencePointManager = GetComponent<ARAnchorManager>();
        m_PlaneManager = GetComponent<ARPlaneManager>();
        m_ReferencePoint = new List<ARAnchor>();
    }


    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = s_Hits[0].pose;
            var planeId = s_Hits[0].trackableId; 
            var referencePoint = m_ReferencePointManager.AttachAnchor(m_PlaneManager.GetPlane(planeId), hitPose);
            if (referencePoint != null)
            {
                RemoveAllAnchors();
                m_ReferencePoint.Add(referencePoint);
            }
        }
    }

    static readonly List<ARRaycastHit> s_Hits = new();

    ARRaycastManager m_RaycastManager;
    ARAnchorManager m_ReferencePointManager;
    List<ARAnchor> m_ReferencePoint;
    ARPlaneManager m_PlaneManager;

}