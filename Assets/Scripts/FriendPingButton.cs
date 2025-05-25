using UnityEngine;
using UnityEngine.UI;

public class FriendPingButton : MonoBehaviour
{
    private FriendPingController pingController; // Reference to the FriendPingController

    void Start()
    {
        // Find the FriendPingController in the scene
        pingController = FindObjectOfType<FriendPingController>();

        // Add a listener to the button
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnPingButtonPressed());
        }
    }

    void OnPingButtonPressed()
    {
        if (pingController != null)
        {
            pingController.OnPingButtonPressed();
        }
        else
        {
            Debug.LogError("FriendPingController not found in the scene!");
        }
    }
}