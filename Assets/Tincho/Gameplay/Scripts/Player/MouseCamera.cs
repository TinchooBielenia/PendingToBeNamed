using UnityEngine;

//TP2 - Martin Bielenia
public class MouseCamera : MonoBehaviour
{
    //This class handles how mouse will move the first person camera.
    private float _xRotation = 0f;

    private float _mouseSensitivity;
    [SerializeField] private float _startMouseSensitivity;
    [SerializeField] private Rigidbody _playerRigidbody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _xRotation = transform.localEulerAngles.x;
        if (_xRotation > 180) _xRotation -= 360;
    }

    void LateUpdate()
    {
        if (Time.timeScale == 0f)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            _mouseSensitivity = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _mouseSensitivity = _startMouseSensitivity;
        }
        MoveCamera();
    }

    private void MoveCamera()
    {
        // Base mouse movement.
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        // Vertical rotation.
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 60f);
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        // Horizontal player rotation.
        Quaternion deltaRotation = Quaternion.Euler(0f, mouseX, 0f);
        _playerRigidbody.MoveRotation(_playerRigidbody.rotation * deltaRotation);
    }

}

