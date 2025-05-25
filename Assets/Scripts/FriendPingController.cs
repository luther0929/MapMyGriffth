using UnityEngine;
using UnityEngine.UI;

public class FriendPingController : MonoBehaviour
{
    public GameObject friendsScreen; // Reference to the FriendsScreen
    public GameObject arModeScreen; // Reference to the ARModeScreen

    // Called when a Ping button is pressed
    public void OnPingButtonPressed()
    {
        // Transition to ARModeScreen
        if (friendsScreen != null && arModeScreen != null)
        {
            friendsScreen.SetActive(false);
            arModeScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("FriendsScreen or ARModeScreen not assigned in FriendPingController!");
        }
    }
}