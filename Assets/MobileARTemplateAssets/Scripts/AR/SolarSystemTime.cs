using UnityEngine;

public class SolarSystemTime : MonoBehaviour
{
    public static SolarSystemTime Instance { get; private set; }

    [Header("Simulation Speed")]
    [Tooltip("Base speed multiplier for all orbits and spins.")]
    [Range(0f, 5f)]
    public float baseSpeed = 1f;

    [Header("State (read-only at runtime)")]
    public bool paused = false;

    public static float TimeScale
    {
        get
        {
            if (Instance == null) return 1f;
            return Instance.paused ? 0f : Instance.baseSpeed;
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

    public void TogglePause()
    {
        paused = !paused;
        Debug.Log($"[SolarSystemTime] Paused = {paused}");
    }

    public void SetPaused(bool value)
    {
        paused = value;
    }

    public void SetSpeed(float speed)
    {
        baseSpeed = Mathf.Max(0f, speed);
    }
}
