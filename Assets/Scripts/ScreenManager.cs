using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject arModeScreen;
    [SerializeField] private GameObject mapScreen;
    [SerializeField] private GameObject profileScreen;
    [SerializeField] private GameObject challengesScreen;
    [SerializeField] private GameObject shopScreen;
    [SerializeField] private GameObject settingsScreen;

    [SerializeField] private Button profileButton;
    [SerializeField] private Button mapButton;
    [SerializeField] private Button challengesButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button settingsButton;

    [Header("Button Colors")]
    [SerializeField] private Color activeColor = Color.cyan;
    [SerializeField] private Color inactiveColor = Color.white;

    private GameObject currentScreen;

    void Start()
    {
        // Add button listeners
        profileButton.onClick.AddListener(() => SwitchScreen(profileScreen, profileButton));
        mapButton.onClick.AddListener(ToggleMapAndARMode);
        challengesButton.onClick.AddListener(() => SwitchScreen(challengesScreen, challengesButton));
        shopButton.onClick.AddListener(() => SwitchScreen(shopScreen, shopButton));
        settingsButton.onClick.AddListener(() => SwitchScreen(settingsScreen, settingsButton));

        // Show AR Mode screen initially
        currentScreen = arModeScreen;
        SwitchScreen(arModeScreen, mapButton); // Map button controls AR mode too
    }

    void SwitchScreen(GameObject targetScreen, Button activeButton)
    {
        // Disable all screens
        arModeScreen.SetActive(false);
        mapScreen.SetActive(false);
        profileScreen.SetActive(false);
        challengesScreen.SetActive(false);
        shopScreen.SetActive(false);
        settingsScreen.SetActive(false);

        // Reset all button colors
        SetButtonColor(profileButton, inactiveColor);
        SetButtonColor(mapButton, inactiveColor);
        SetButtonColor(challengesButton, inactiveColor);
        SetButtonColor(shopButton, inactiveColor);
        SetButtonColor(settingsButton, inactiveColor);

        // Enable target screen and highlight active button
        targetScreen.SetActive(true);
        SetButtonColor(activeButton, activeColor);

        currentScreen = targetScreen;
    }

    void ToggleMapAndARMode()
    {
        if (currentScreen == arModeScreen)
        {
            SwitchScreen(mapScreen, mapButton);
        }
        else
        {
            SwitchScreen(arModeScreen, mapButton);
        }
    }

    // Helper method to set button colors
    void SetButtonColor(Button button, Color color)
    {
        // Get the button's image component
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = color;
        }

        // Also try to find and color any child images (icons)
        Image[] childImages = button.GetComponentsInChildren<Image>();
        foreach (Image image in childImages)
        {
            // Skip the button's own image to avoid double-coloring
            if (image != buttonImage)
            {
                image.color = color;
            }
        }
    }
}