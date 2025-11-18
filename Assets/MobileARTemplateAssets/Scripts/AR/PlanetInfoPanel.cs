using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetInfoPanel : MonoBehaviour
{
    public static PlanetInfoPanel Instance { get; private set; }

    [Header("References")]
    public GameObject root;    
    public TMP_Text titleText;
    public TMP_Text bodyText;
    public Image iconImage;

    void Awake()
    {
        Debug.Log("[PlanetInfoPanel] Awake on " + gameObject.name);

        if (Instance != null && Instance != this)
        {
            Debug.Log("[PlanetInfoPanel] Another instance exists, destroying this one.");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (root == null)
            root = gameObject;

        Hide();
    }

    public void Show(PlanetInfo info)
    {
        if (info == null) return;

        root.SetActive(true);

        if (titleText != null)
            titleText.text = string.IsNullOrEmpty(info.displayName)
                ? info.name
                : info.displayName;

        if (bodyText != null)
            bodyText.text = info.description;

        if (iconImage != null)
        {
            if (info.icon != null)
            {
                iconImage.sprite = info.icon;
                iconImage.enabled = true;
            }
            else
            {
                iconImage.enabled = false;
            }
        }
    }

    public void Hide()
    {
        if (root != null)
            root.SetActive(false);
    }

    public void OnCloseButtonClicked()
    {
        Hide();
    }
}
