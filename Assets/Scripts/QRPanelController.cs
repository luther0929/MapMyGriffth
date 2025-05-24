using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QRPanelController : MonoBehaviour
{
    public GameObject qrPanel; // Reference to the QRPanel GameObject
    public Button shareQRButton; // Reference to the Share QR button
    public Button backButton; // Reference to the Back (XB) button
    private CanvasGroup qrCanvasGroup; // Reference to the CanvasGroup for fading

    void Start()
    {
        // Get the CanvasGroup component
        qrCanvasGroup = qrPanel.GetComponent<CanvasGroup>();
        if (qrCanvasGroup != null)
        {
            qrCanvasGroup.alpha = 0f; // Start with the panel invisible
            qrPanel.SetActive(false); // Ensure it's inactive
        }

        // Add listeners for button clicks
        if (shareQRButton != null)
        {
            shareQRButton.onClick.AddListener(ShowQRPanel);
        }

        if (backButton != null)
        {
            backButton.onClick.AddListener(HideQRPanel);
        }
    }

    // Function to show the QR panel with a fade-in effect
    void ShowQRPanel()
    {
        if (qrPanel != null)
        {
            qrPanel.SetActive(true);
            StartCoroutine(FadeCanvasGroup(qrCanvasGroup, qrCanvasGroup.alpha, 1f, 0.3f));
        }
    }

    // Function to hide the QR panel with a fade-out effect
    void HideQRPanel()
    {
        if (qrPanel != null)
        {
            StartCoroutine(FadeCanvasGroup(qrCanvasGroup, qrCanvasGroup.alpha, 0f, 0.3f, () => qrPanel.SetActive(false)));
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