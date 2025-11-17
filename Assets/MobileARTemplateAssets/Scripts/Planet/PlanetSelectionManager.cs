using UnityEngine;

public class PlanetSelectionManager : MonoBehaviour
{
    private Camera arCam;
    private PlanetHighlight current;

    [Tooltip("Tap radius in screen pixels for selecting a planet.")]
    public float tapRadiusPx = 80f;

    private PlanetHighlight[] allPlanets;

    void Start()
    {
        arCam = Camera.main;
        RefreshPlanets();
    }

    public void RefreshPlanets()
    {
        allPlanets = Object.FindObjectsByType<PlanetHighlight>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None);

        Debug.Log($"PlanetSelectionManager: found {allPlanets.Length} planets.");
    }

    public bool TrySelectPlanet(Vector2 screenPos)
    {
        if (arCam == null || allPlanets == null || allPlanets.Length == 0)
            return false;

        PlanetHighlight best = null;
        float bestDist = float.MaxValue;

        foreach (var planet in allPlanets)
        {
            if (planet == null) continue;

            Vector3 planetScreenPos = arCam.WorldToScreenPoint(planet.transform.position);

            if (planetScreenPos.z < 0f) continue;

            float dist = Vector2.Distance(screenPos, planetScreenPos);

            if (dist < tapRadiusPx && dist < bestDist)
            {
                bestDist = dist;
                best = planet;
            }
        }

        if (best != null)
        {
            if (current != null)
                current.Unhighlight();

            current = best;
            current.Highlight();
            Debug.Log("Selected planet: " + best.name);
            return true;
        }

        return false;
    }
}
