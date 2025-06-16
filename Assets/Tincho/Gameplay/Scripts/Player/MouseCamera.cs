using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    //This class handles how mouse will move the first person camera.
    private float xRotation = 0f;

    private float mouseSensitivity;
    [SerializeField] private float _startMouseSensitivity;
    [SerializeField] private Rigidbody _playerRigidbody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        xRotation = transform.localEulerAngles.x;
        if (xRotation > 180) xRotation -= 360;
    }

    void LateUpdate()
    {
        if (Time.timeScale == 0f)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            mouseSensitivity = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mouseSensitivity = _startMouseSensitivity;
        }

        // Base mouse movement.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Vertical rotation.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 60f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal player rotation.
        Quaternion deltaRotation = Quaternion.Euler(0f, mouseX, 0f);
        _playerRigidbody.MoveRotation(_playerRigidbody.rotation * deltaRotation);
    }
}

