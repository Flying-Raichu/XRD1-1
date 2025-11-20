using UnityEngine;
using TMPro;

public class PauseButtonUI : MonoBehaviour
{
    [Tooltip("Optional label to update with speed text.")]
    public TMP_Text label;

    void Start()
    {
        if (label == null)
            label = GetComponentInChildren<TMP_Text>();

        UpdateLabel();
    }

    public void OnPauseButtonClicked()
    {
        if (SolarSystemTime.Instance == null)
        {
            Debug.LogWarning("[PauseButtonUI] SolarSystemTime.Instance is null");
            return;
        }

        SolarSystemTime.Instance.CycleState();
        UpdateLabel();
    }

    void UpdateLabel()
    {
        if (label == null || SolarSystemTime.Instance == null) return;

        switch (SolarSystemTime.Instance.state)
        {
            case SolarSystemTime.SimState.Paused:
                label.text = "Paused";
                break;
            case SolarSystemTime.SimState.VerySlowSpeed:
                label.text = "Speed: 0.2x";
                break;
            case SolarSystemTime.SimState.SlowSpeed:
                label.text = "Speed: 0.5x";
                break;
            case SolarSystemTime.SimState.Normal:
            default:
                label.text = "Speed: 1x";
                break;
        }
    }
}
