using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    // This class hanldes how the player moves in the world.
    private Movement _movementHandler;

    [Header("Movement")]
    [SerializeField]
    private float _ogSpeed;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _sprintMultiplier = 1.5f;
    private float _xAxis, _zAxis, _yAxis;
    private Rigidbody _rb;
    private bool _isMoving;
    private PlayerStats _playerStats;
    private Animator _animator;
    public int zAxisDirection = 1;
    public bool isMoving => _isMoving;
    public bool isSprinting;

    //[SerializeField] private int jumpForce;
    //private bool _isGrounded;
    //private bool _isJumping;

    [Header("SFX")]
    public AudioSource footstepsSFX;
    public AudioSource footstepsSprintFX;
    public AudioSource heavyBreathingFX;



    private void Start()
    {
        //jumpForce = 3;

        _speed = _ogSpeed;

        _playerStats = GetComponent<PlayerStats>();

        isSprinting = false;

        _animator = GetComponentInChildren<Animator>();

        heavyBreathingFX.enabled = false;

        _rb = GetComponent<Rigidbody>();


        //Values sent to constructor.
        _movementHandler = new Movement(
            _animator,
            _rb,
            footstepsSFX,
            footstepsSprintFX,
            heavyBreathingFX,
            _ogSpeed,
            _sprintMultiplier,
            transform,
            _playerStats
        );
    }

    void Update()
    {
        // Inputs.
        Vector2 input = InputController.Instance.MoveInput;
        _xAxis = input.x;
        _zAxis = input.y * zAxisDirection;

        //if (InputController.Instance.JumpPressed) && _isGrounded)
        //{
        //    _rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.z);
        //}

        _movementHandler.MoveAndSprint(_xAxis, _zAxis);
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    Debug.Log("El jugador esta en el piso.");
    //    _isGrounded = true;
    //    _isJumping = true;
    //    _animator.SetBool("isJumping", true);
    //}

    // This method inverts the zAxis at the moment player interacts with the InverterObject.
    public void InvertZAxis(bool state)
    {
        if (state)
        {
            zAxisDirection *= -1;
        }
        else
        {
            zAxisDirection *= -1;
        }
    }
}
