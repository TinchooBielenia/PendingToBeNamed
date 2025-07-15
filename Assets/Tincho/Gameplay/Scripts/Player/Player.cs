using UnityEngine;

//TP2 - Martin Bielenia - Juliana Dimeglio
public class Player : MonoBehaviour
{
    public static Player Instance;
    // This class hanldes how the player moves in the world.
    private Movement _movementHandler;

    [Header("References")]
    private PlayerHealth _playerHealth;
    private PlayerShootStats _playerShootStats;
    private WeaponPickup _weaponPickup;

    public bool PlayerHealth => _playerHealth;
    public bool PlayerShootStats => _playerShootStats;
    public bool WeaponPickup => _weaponPickup;

    [Header("Movement")]
    [SerializeField]
    private float _ogSpeed;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _sprintMultiplier = 1.5f;
    private float _xAxis, _zAxis;
    private Rigidbody _rb;
    private PlayerStaminaStats _playerStats;
    private Animator _animator;
    [SerializeField] private MouseCamera _mouseCamera;

    [Header("SFX")]
    [SerializeField] private AudioSource _footstepsSFX;
    [SerializeField] private AudioSource _footstepsSprintFX;
    [SerializeField] private AudioSource _heavyBreathingFX;
    [SerializeField] private AudioSource _deathSFX;

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

    private void Start()
    {
        _speed = _ogSpeed;

        _playerStats = GetComponent<PlayerStaminaStats>();

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

    void FixedUpdate()
    {
        // Inputs.
        Vector2 input = InputController.Instance.MoveInput;
        _xAxis = input.x;
        _zAxis = input.y;

        _movementHandler.MoveAndSprint(_xAxis, _zAxis);
    }

    public void Die()
    {
        FrozenPlayer();
        _animator.applyRootMotion = true;
        _animator.SetTrigger("Death");
        _deathSFX.Play();
        SceneHanlder.Instance.OnPlayerDeath();
    }

    public void FrozenPlayer()
    {
        _movementHandler.DisableMovement();
        _footstepsSFX.Stop();
        _footstepsSprintFX.Stop();
        _heavyBreathingFX.Stop();
        _mouseCamera.enabled = false;
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }
    }
}
