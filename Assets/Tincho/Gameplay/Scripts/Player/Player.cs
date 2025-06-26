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
    private PlayerStaminaStats _playerStats;
    private Animator _animator;
    public int zAxisDirection = 1;
    public bool isMoving => _isMoving;
    public bool isSprinting;

    [Header("SFX")]
    [SerializeField] private AudioSource _footstepsSFX;
    [SerializeField] private AudioSource _footstepsSprintFX;
    [SerializeField] private AudioSource _heavyBreathingFX;
    [SerializeField] private AudioSource _deathSFX;



    private void Start()
    {
        _speed = _ogSpeed;

        _playerStats = GetComponent<PlayerStaminaStats>();

        isSprinting = false;

        _animator = GetComponentInChildren<Animator>(); 

        _heavyBreathingFX.enabled = false;

        _rb = GetComponent<Rigidbody>();


        //Values sent to constructor.
        _movementHandler = new Movement(
            _animator,
            _rb,
            _footstepsSFX,
            _footstepsSprintFX,
            _heavyBreathingFX,
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

        _movementHandler.MoveAndSprint(_xAxis, _zAxis);
    }

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
    public void Die()
    {
        _animator.applyRootMotion = true;

        _animator.SetTrigger("Death");

        enabled = false; 
        _movementHandler.DisableMovement(); 

        _footstepsSFX.Stop();
        _footstepsSprintFX.Stop();
        _heavyBreathingFX.Stop();
        _deathSFX.Play();

        FindObjectOfType<MouseCamera>().enabled = false;

        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }
    }

}
