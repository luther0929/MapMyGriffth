using UnityEngine;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    [SerializeField] private int playerPoints = 1000;
    [SerializeField] private TextMeshProUGUI pointsText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        UpdatePointsUI();
    }

    public bool DeductPoints(int amount)
    {
        if (playerPoints >= amount)
        {
            playerPoints -= amount;
            UpdatePointsUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough points.");
            return false;
        }
    }

    public void UpdatePointsUI()
    {
        if (pointsText != null)
            pointsText.text = playerPoints + " Points";
    }

    public int GetPoints()
    {
        return playerPoints;
    }
}
