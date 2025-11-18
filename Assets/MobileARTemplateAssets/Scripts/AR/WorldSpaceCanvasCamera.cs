using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class WorldSpaceCanvasCamera : MonoBehaviour
{
    void Awake()
    {
        var canvas = GetComponent<Canvas>();
        if (canvas.renderMode == RenderMode.WorldSpace)
        {
            if (canvas.worldCamera == null && Camera.main != null)
            {
                canvas.worldCamera = Camera.main;
            }
        }
    }
}
