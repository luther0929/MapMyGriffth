using UnityEngine;
using UnityEngine.UI;

public class ShopScreenTabManager : MonoBehaviour
{
    [SerializeField] private GameObject rewardsPanel;
    [SerializeField] private GameObject themesPanel;
    [SerializeField] private Button rewardsTabButton;
    [SerializeField] private Button themesTabButton;

    [Header("Tab Colors")]
    [SerializeField] private Color activeColor = Color.grey;
    [SerializeField] private Color inactiveColor = Color.white;

    private void Start()
    {
        rewardsTabButton.onClick.AddListener(ShowRewardsTab);
        themesTabButton.onClick.AddListener(ShowThemesTab);

        ShowRewardsTab(); // Show rewards by default
    }

    private void ShowRewardsTab()
    {
        rewardsPanel.SetActive(true);
        themesPanel.SetActive(false);

        SetButtonColor(rewardsTabButton, activeColor);
        SetButtonColor(themesTabButton, inactiveColor);
    }

    private void ShowThemesTab()
    {
        rewardsPanel.SetActive(false);
        themesPanel.SetActive(true);

        SetButtonColor(rewardsTabButton, inactiveColor);
        SetButtonColor(themesTabButton, activeColor);
    }

    private void SetButtonColor(Button button, Color color)
    {
        var btnImage = button.GetComponent<Image>();
        if (btnImage != null)
            btnImage.color = color;
    }
}
