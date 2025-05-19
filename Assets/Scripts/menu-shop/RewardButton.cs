using UnityEngine;
using UnityEngine.UI;

public class RewardButton : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private Button redeemButton;

    private void Start()
    {
        redeemButton.onClick.AddListener(RedeemReward);
    }

    private void RedeemReward()
    {
        if (PlayerData.Instance.DeductPoints(cost))
        {
            Debug.Log($"Redeemed for {cost} points!");
        }
    }
}
