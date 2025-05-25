using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ThemePanelController : MonoBehaviour
{
    public GameObject chooseThemePanel; // Reference to the ChooseThemePanel GameObject
    public Button changeThemeButton; // Reference to the Change button under Theme
    public Button confirmButton; // Reference to the Confirm button in ChooseThemePanel
    public Button defaultButton; // Reference to the Default theme button
    public Button griffithButton; // Reference to the Griffith theme button
    public Button ninetiesButton; // Reference to the 90s theme button
    public Button cyberpunkButton; // Reference to the Cyberpunk theme button
    public List<Image> uiPanels; // List of main UI panels to apply the theme to (e.g., ProfileScreen, ChallengesScreen, etc.)
    public List<Image> subPanels; // List of subpanel UI images to apply the theme to (e.g., QRPanel, profile info, etc.)

    private CanvasGroup themeCanvasGroup; // Reference to the CanvasGroup for fading
    private Button selectedThemeButton; // Track the currently selected theme button
    private Color highlightedColor = new Color32(255, 255, 255, 100); // Highlight color (255, 255, 255, 100)
    private Color defaultColor = new Color32(111, 111, 111, 100); // Default color for theme buttons (111, 111, 111, 100)

    // Preset colors for each theme
    private Color defaultMainColor = new Color32(55, 55, 55, 255); // Default main panels (55, 55, 55, 255)
    private Color defaultSubColor = new Color32(22, 22, 22, 255); // Default subpanels (22, 22, 22, 255)
    private Color griffithMainColor = new Color32(90, 21, 21, 255); // Griffith main panels (90, 21, 21, 255)
    private Color griffithSubColor = new Color32(170, 56, 56, 255); // Griffith subpanels (170, 56, 56, 255)
    private Color ninetiesMainColor = new Color32(200, 80, 120, 255); // 90s main panels (200, 80, 120, 255)
    private Color ninetiesSubColor = new Color32(40, 140, 140, 255); // 90s subpanels (40, 140, 140, 255)
    private Color cyberpunkMainColor = new Color32(0, 50, 60, 255); // Cyberpunk main panels (0, 50, 60, 255)
    private Color cyberpunkSubColor = new Color32(0, 90, 110, 255); // Cyberpunk subpanels (0, 90, 110, 255)

    private string selectedTheme; // Track the selected theme name

    void Start()
    {
        // Get the CanvasGroup component
        themeCanvasGroup = chooseThemePanel.GetComponent<CanvasGroup>();
        if (themeCanvasGroup != null)
        {
            themeCanvasGroup.alpha = 0f; // Start with the panel invisible
            chooseThemePanel.SetActive(false); // Ensure it's inactive
        }

        // Add listeners for SettingsScreen buttons
        if (changeThemeButton != null)
        {
            changeThemeButton.onClick.AddListener(ShowThemePanel);
        }

        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(HideThemePanel);
        }

        // Add listeners for theme buttons and set default colors
        if (defaultButton != null)
        {
            defaultButton.GetComponent<Image>().color = highlightedColor; // Highlight Default as initially selected
            defaultButton.onClick.AddListener(() => SelectTheme(defaultButton, "Default"));
        }

        if (griffithButton != null)
        {
            griffithButton.GetComponent<Image>().color = defaultColor;
            griffithButton.onClick.AddListener(() => SelectTheme(griffithButton, "Griffith"));
        }

        if (ninetiesButton != null)
        {
            ninetiesButton.GetComponent<Image>().color = defaultColor;
            ninetiesButton.onClick.AddListener(() => SelectTheme(ninetiesButton, "90s"));
        }

        if (cyberpunkButton != null)
        {
            cyberpunkButton.GetComponent<Image>().color = defaultColor;
            cyberpunkButton.onClick.AddListener(() => SelectTheme(cyberpunkButton, "Cyberpunk"));
        }

        // Set Default as the initial theme
        if (string.IsNullOrEmpty(selectedTheme))
        {
            selectedTheme = "Default"; // Default theme on start
            selectedThemeButton = defaultButton; // Set Default button as initially selected
            ApplyTheme();
        }
    }

    // Function to show the Theme panel with a fade-in effect
    void ShowThemePanel()
    {
        if (chooseThemePanel != null)
        {
            chooseThemePanel.SetActive(true);
            StartCoroutine(FadeCanvasGroup(themeCanvasGroup, themeCanvasGroup.alpha, 1f, 0.3f));
        }
    }

    // Function to hide the Theme panel with a fade-out effect and apply the theme
    void HideThemePanel()
    {
        if (chooseThemePanel != null)
        {
            StartCoroutine(FadeCanvasGroup(themeCanvasGroup, themeCanvasGroup.alpha, 0f, 0.3f, () =>
            {
                chooseThemePanel.SetActive(false);
                ApplyTheme(); // Apply the selected theme when the panel is closed
            }));
        }
    }

    // Function to handle theme selection and highlighting
    void SelectTheme(Button themeButton, string themeName)
    {
        // If a theme is already selected, revert its color to default
        if (selectedThemeButton != null && selectedThemeButton != themeButton)
        {
            selectedThemeButton.GetComponent<Image>().color = defaultColor;
        }

        // Highlight the newly selected theme
        themeButton.GetComponent<Image>().color = highlightedColor;
        selectedThemeButton = themeButton; // Update the selected theme button
        selectedTheme = themeName; // Update the selected theme name
    }

    // Function to apply the selected theme to all UI panels and subpanels
    void ApplyTheme()
    {
        Color mainColor, subPanelColor;

        // Determine the colors based on the selected theme
        switch (selectedTheme)
        {
            case "Default":
                mainColor = defaultMainColor; // Different colors for main and subpanels
                subPanelColor = defaultSubColor;
                break;
            case "Griffith":
                mainColor = griffithMainColor; // Different colors for main and subpanels
                subPanelColor = griffithSubColor;
                break;
            case "90s":
                mainColor = ninetiesMainColor; // Different colors for main and subpanels
                subPanelColor = ninetiesSubColor;
                break;
            case "Cyberpunk":
                mainColor = cyberpunkMainColor; // Different colors for main and subpanels
                subPanelColor = cyberpunkSubColor;
                break;
            default:
                mainColor = defaultMainColor;
                subPanelColor = defaultSubColor; // Fallback to Default
                break;
        }

        // Apply the color to all main UI panels
        foreach (Image panel in uiPanels)
        {
            if (panel != null)
            {
                panel.color = mainColor;
            }
        }

        // Apply the color to all subpanels
        foreach (Image subPanel in subPanels)
        {
            if (subPanel != null)
            {
                subPanel.color = subPanelColor;
            }
        }
    }

    // Coroutine to fade the CanvasGroup
    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float start, float end, float duration, System.Action onComplete = null)
    {
        float elapsedTime = 0f;
        canvasGroup.alpha = start;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }

        canvasGroup.alpha = end;
        onComplete?.Invoke(); // Call the onComplete action if provided
    }
}