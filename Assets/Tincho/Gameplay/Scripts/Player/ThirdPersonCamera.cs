using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target; // El CameraPivot
    [SerializeField] private LayerMask collisionLayers;

    [Header("Camera Settings")]
    [SerializeField] private float distance = 4f;
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private Vector2 pitchLimits = new Vector2(-30, 60);
    [SerializeField] private float mouseSensitivity = 2f;

    private float yaw;
    private float pitch;

    void LateUpdate()
    {
        HandleRotation();
        HandleCollision();
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);

        target.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void HandleCollision()
    {
        Vector3 desiredPosition = target.position - target.forward * distance;

        RaycastHit hit;
        if (Physics.Linecast(target.position, desiredPosition, out hit, collisionLayers))
        {
            float adjustedDistance = Mathf.Clamp(hit.distance - 0.2f, minDistance, distance);
            desiredPosition = target.position - target.forward * adjustedDistance;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        transform.LookAt(target);
    }
}
