using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PauseButtonUI : MonoBehaviour
{
    [Tooltip("Optional label to update with Play/Pause text.")]
    public TMP_Text label;

    void Start()
    {
        if (label == null)
            label = GetComponentInChildren<TMP_Text>();

        UpdateLabel();
    }

    public void OnPauseButtonClicked()
    {
        if (SolarSystemTime.Instance == null) return;

        SolarSystemTime.Instance.TogglePause();
        UpdateLabel();
    }

    void UpdateLabel()
    {
        if (label == null || SolarSystemTime.Instance == null) return;

        label.text = SolarSystemTime.Instance.paused ? "Play" : "Pause";
    }
}
