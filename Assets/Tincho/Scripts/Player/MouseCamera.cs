using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    //This class handles how mouse will move the first person camera.
    private float xRotation = 0f;
    private Vector2 currentMouse;
    private Vector2 currentMouseSpeed;

    public float mouseSensitivity = 100f;
    //public float smoothTime = 0.05f; // Movement smoothness.
    public Rigidbody playerRigidbody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        xRotation = transform.localEulerAngles.x;
        if (xRotation > 180) xRotation -= 360;
    }

    void LateUpdate()
    {
        // Base mouse movement.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical rotation.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 60f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal player rotation.
        Quaternion deltaRotation = Quaternion.Euler(0f, mouseX, 0f);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * deltaRotation);
    }
}

