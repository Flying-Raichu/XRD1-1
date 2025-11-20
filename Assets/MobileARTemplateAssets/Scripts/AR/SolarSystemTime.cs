using UnityEngine;

public class SolarSystemTime : MonoBehaviour
{
    public static SolarSystemTime Instance { get; private set; }

    public enum SimState
    {
        Paused,
        VerySlowSpeed,
        SlowSpeed,
        Normal
    }

    [Header("Speeds")]
    [Tooltip("Speed when in VerySlowSpeed mode.")]
    public float verySlowSpeed = 0.2f;

    [Tooltip("Speed when in SlowSpeed mode.")]
    public float slowSpeed = 0.5f;

    [Tooltip("Speed when in Normal mode.")]
    public float normalSpeed = 1f;

    [Header("Current State (read-only at runtime)")]
    public SimState state = SimState.Normal;

    public static float TimeScale
    {
        get
        {
            if (Instance == null) return 1f;

            switch (Instance.state)
            {
                case SimState.Paused: return 0f;
                case SimState.VerySlowSpeed: return Instance.verySlowSpeed;
                case SimState.SlowSpeed: return Instance.slowSpeed;
                case SimState.Normal:
                default: return Instance.normalSpeed;
            }
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CycleState()
    {
        switch (state)
        {
            case SimState.Paused:
                state = SimState.VerySlowSpeed;
                break;
            case SimState.VerySlowSpeed:
                state = SimState.SlowSpeed;
                break;
            case SimState.SlowSpeed:
                state = SimState.Normal;
                break;
            case SimState.Normal:
                state = SimState.Paused;
                break;
            default:
                state = SimState.Normal;
                break;
        }

        Debug.Log("[SolarSystemTime] New state: " + state + " (TimeScale=" + TimeScale + ")");
    }
}
