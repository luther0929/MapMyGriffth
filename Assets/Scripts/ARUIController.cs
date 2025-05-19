using UnityEngine;
using UnityEngine.UI;

public class ARUIController : MonoBehaviour
{
    [SerializeField] private ARUIArrow uiArrow;
    [SerializeField] private Button setTargetButton;

    void Start()
    {
        Debug.Log("ARUIController started");

        if (setTargetButton != null)
        {
            setTargetButton.onClick.AddListener(OnSetTargetButtonClicked);
            Debug.Log("Set Target Button listener added to: " + setTargetButton.name);
        }
        else
        {
            Debug.LogError("Set Target Button not assigned!");
        }

        if (uiArrow == null)
        {
            uiArrow = FindObjectOfType<ARUIArrow>();
            if (uiArrow != null)
            {
                Debug.Log("UI Arrow found automatically: " + uiArrow.name);
            }
            else
            {
                Debug.LogError("UI Arrow not found in scene!");
                return;
            }
        }

        // Force initial target for testing
        SetRandomTarget();
    }

    void Update()
    {
        // Test key for changing target
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed - changing target");
            SetRandomTarget();
        }
    }

    void OnSetTargetButtonClicked()
    {
        Debug.Log("===== Set Target Button clicked =====");
        SetRandomTarget();
    }

    private void SetRandomTarget()
    {
        if (uiArrow == null)
        {
            Debug.LogError("UI Arrow reference is missing!");
            return;
        }

        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        // Set a random target position to test arrow rotation
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0).normalized;

        Vector3 targetWorldPos = mainCamera.transform.position +
                                mainCamera.transform.forward * 10f +
                                randomDirection * 5f;

        Debug.Log("Setting target at world position: " + targetWorldPos);

        // Set the target for the UI arrow
        uiArrow.SetTarget(targetWorldPos);
    }
}