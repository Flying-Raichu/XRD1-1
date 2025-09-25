//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;

//[RequireComponent(typeof(ARTrackedImageManager))]
//public class TrackedImageSunSpawner : MonoBehaviour
//{
//    public GameObject sunPrefab;

//    ARTrackedImageManager manager;
//    readonly Dictionary<string, GameObject> spawned = new();

//    void Awake() => manager = GetComponent<ARTrackedImageManager>();
//    void OnEnable() => manager.trackablesChanged += OnChanged;
//    void OnDisable() => manager.trackablesChanged -= OnChanged;

//    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventSettings)
//    {
//        foreach (var img in eventSettings.added) CreateOrUpdate(img);
//        foreach (var img in eventSettings.updated) CreateOrUpdate(img);

//        foreach (var img in eventSettings.removed)
//        {
//            if (spawned.TryGetValue(img.referenceImage.name, out var go))
//            {
//                Destroy(go);
//                spawned.Remove(img.referenceImage.name);
//            }
//        }
//    }

//    void CreateOrUpdate(ARTrackedImage img)
//    {
//        var key = img.referenceImage.name;

//        if (!spawned.TryGetValue(key, out var go))
//        {
//            go = Instantiate(sunPrefab, img.transform);
//            go.name = $"Sun_{key}";
//            spawned[key] = go;
//        }

//        go.SetActive(img.trackingState == TrackingState.Tracking);
//        go.transform.localPosition = Vector3.zero;
//        go.transform.localRotation = Quaternion.identity;

//        float width = (float)img.size.x; // meters
//        go.transform.localScale = Vector3.one * (width * 0.35f);
//    }
//}
