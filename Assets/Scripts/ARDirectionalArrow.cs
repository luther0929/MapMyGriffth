using UnityEngine;

public class ARDirectionalArrow : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 80.0f;

    private Vector3 targetPosition;
    private bool hasTarget = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Store the initial transform values
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Keep the arrow visible
        gameObject.SetActive(true);

        Debug.Log("Arrow positioned at: " + initialPosition);
    }

    void Update()
    {
        // Maintain the exact position you set in the editor
        transform.position = initialPosition;

        // Only rotate if we have a target
        if (hasTarget)
        {
            // Calculate direction to target, but only in the XY plane (for Z-axis rotation)
            Vector3 direction = targetPosition - transform.position;

            // Project the direction onto the XY plane
            Vector3 directionXY = new Vector3(direction.x, direction.y, 0);

            if (directionXY.magnitude > 0.1f)
            {
                // Calculate the angle in the XY plane (for Z-axis rotation)
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Create a rotation that rotates around the Z axis
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90); // -90 to make it point forward

                // Smoothly rotate to the target rotation
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime);

                // Debug visualization
                Debug.DrawRay(transform.position, directionXY.normalized * 5f, Color.blue);
            }
        }
        else
        {
            // Reset to initial rotation if no target
            transform.rotation = initialRotation;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
        Debug.Log("Arrow target set to: " + position);
    }

    public void ClearTarget()
    {
        hasTarget = false;
        Debug.Log("Arrow target cleared");
    }
}