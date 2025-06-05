using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public Vector2 MoveInput => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    public bool IsSprinting => Input.GetKey(KeyCode.LeftShift);
    public bool JumpPressed => Input.GetKeyDown(KeyCode.Space);
    public bool InteractPressed => Input.GetKeyDown(KeyCode.E);
    public bool EscapeKey => Input.GetKeyDown(KeyCode.Escape);
    // Agregá más inputs según necesites
}
