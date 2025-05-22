using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TextMeshPro
using System.Collections; // Added for IEnumerator

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject arModeScreen;
    [SerializeField] private GameObject mapScreen;
    [SerializeField] private GameObject profileScreen;
    [SerializeField] private GameObject challengesScreen;
    [SerializeField] private GameObject shopScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject mapImage; // Reference to MapImage

    [SerializeField] private Button profileButton;
    [SerializeField] private Button mapButton;
    [SerializeField] private Button challengesButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button settingsButton;

    // UI elements for ARmodeScreen messages
    [SerializeField] private GameObject challengeMessage; // "Challenge accepted" text
    [SerializeField] private TextMeshProUGUI countdownText; // Countdown text (3, 2, 1, GO!)

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

        // Show AR Mode screen initially and hide MapImage
        currentScreen = arModeScreen;
        SwitchScreen(arModeScreen, mapButton); // Map button controls AR mode too
        if (mapImage != null) mapImage.SetActive(false);

        // Ensure message and countdown are hidden initially
        if (challengeMessage != null) challengeMessage.SetActive(false);
        else Debug.LogWarning("ChallengeMessage not assigned in ScreenManager!");
        if (countdownText != null) countdownText.gameObject.SetActive(false);
        else Debug.LogWarning("CountdownText not assigned in ScreenManager!");
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

        // Toggle MapImage visibility based on whether MapScreen is the target
        if (mapImage != null)
        {
            bool isMapScreen = (targetScreen == mapScreen);
            mapImage.SetActive(isMapScreen);
            Debug.Log($"MapImage set to {(isMapScreen ? "active" : "inactive")} for screen: {targetScreen.name}");
        }

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

    // Method to handle challenge tap
    public void OnChallengeTapped()
    {
        // Switch to ARmodeScreen
        SwitchScreen(arModeScreen, mapButton);

        // Start the message and countdown sequence
        StartCoroutine(ShowChallengeSequence());
    }

    IEnumerator ShowChallengeSequence()
    {
        // Show "Challenge accepted" message for 1 second
        if (challengeMessage != null)
        {
            challengeMessage.SetActive(true);
            yield return new WaitForSeconds(1f);
            challengeMessage.SetActive(false);
        }
        else
        {
            Debug.LogWarning("ChallengeMessage is null, skipping message display.");
        }

        // Show countdown: 3, 2, 1, GO!
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
            countdownText.text = "3";
            yield return new WaitForSeconds(1f);
            countdownText.text = "2";
            yield return new WaitForSeconds(1f);
            countdownText.text = "1";
            yield return new WaitForSeconds(1f);
            countdownText.text = "GO!";
            yield return new WaitForSeconds(1f);
            countdownText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("CountdownText is null, skipping countdown.");
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