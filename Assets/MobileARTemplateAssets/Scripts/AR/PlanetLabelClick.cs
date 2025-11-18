using UnityEngine;
using UnityEngine.EventSystems;

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
