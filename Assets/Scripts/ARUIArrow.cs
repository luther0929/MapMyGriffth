using UnityEngine;
using UnityEngine.UI;

public class ARUIArrow : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 300.0f;

    private Vector3 targetPosition;
    private bool hasTarget = false;
    private Camera mainCamera;

    void Start()
    {
        Debug.Log("ARUIArrow: Start called");
        mainCamera = Camera.main;
        gameObject.SetActive(true);
    }

    void Update()
    {
        // Don't apply continuous rotation anymore
        // transform.Rotate(0, 0, 10 * Time.deltaTime);

        if (!hasTarget)
        {
            // No need to log this every frame
            return;
        }

        Debug.Log("ARUIArrow: Has target: " + targetPosition);

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null) return;
        }

        // Calculate direction to target
        Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(targetPosition);

        // Don't process if behind camera
        if (targetScreenPos.z < 0) return;

        // Get screen center
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        // Direction from center to target
        Vector2 direction = new Vector2(targetScreenPos.x - screenCenter.x, targetScreenPos.y - screenCenter.y);

        if (direction.magnitude > 1.0f)
        {
            // Calculate angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Set rotation directly - no smoothing for now
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            Debug.Log("ARUIArrow: Pointing at angle: " + (angle - 90));
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
        Debug.Log("ARUIArrow: Target set to: " + position);
    }

    public void ClearTarget()
    {
        hasTarget = false;
        Debug.Log("ARUIArrow: Target cleared");
    }
}