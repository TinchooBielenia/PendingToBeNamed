using UnityEngine;

//TP2 - Martin Bielenia
public class Movement
{
    // This class handles the movement in the player. It receives the values from PlayerMovement and use them here.

    private Animator _animator;
    private Rigidbody _rb;
    private AudioSource _footstepsSFX;
    private AudioSource _footstepsSprintFX;
    private AudioSource _heavyBreathingFX;
    private float _ogSpeed;
    private float _sprintMultiplier;
    private float _speed;
    private Transform _transform;
    private PlayerStaminaStats _playerStats;
    private bool _canMove = true;


    public bool IsMoving { get; private set; }

    public Movement(
        Animator animator,
        Rigidbody rb,
        AudioSource footstepsSFX,
        AudioSource footstepsSprintFX,
        AudioSource heavyBreathingFX,
        float ogSpeed,
        float sprintMultiplier,
        Transform transform,
        PlayerStaminaStats playerStats)
    {
        _animator = animator;
        _rb = rb;
        _footstepsSFX = footstepsSFX;
        _footstepsSprintFX = footstepsSprintFX;
        _heavyBreathingFX = heavyBreathingFX;
        _ogSpeed = ogSpeed;
        _sprintMultiplier = sprintMultiplier;
        _transform = transform;
        _playerStats = playerStats;
        _speed = ogSpeed;
    }

    public void MoveAndSprint(float x, float z)
    {
        if (!_canMove) return;

        // Verifica si hay input de movimiento ANTES de todo.
        bool hasInput = x != 0 || z != 0;

        // Dirección y movimiento.
        Vector3 dir = (_transform.right * x + _transform.forward * z).normalized;

        if (hasInput)
        {
            Vector3 currentVelocity = _rb.velocity;
            _rb.velocity = new Vector3(dir.x * _speed, currentVelocity.y, dir.z * _speed);
        }
        else
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        }

        _animator.SetFloat("xMov", x);
        _animator.SetFloat("zMov", z);

        // Sprint
        bool isSprinting = InputController.Instance.IsSprinting && _playerStats.CanSprint && hasInput;

        if (isSprinting)
        {
            _animator.SetBool("isSprinting", true);
            _speed = _ogSpeed * _sprintMultiplier;
            _playerStats.UseStamina();
            _playerStats.StaminaIsBeingConsumed = true;
        }
        else
        {
            _animator.SetBool("isSprinting", false);
            _speed = _ogSpeed;
            _playerStats.RecoverStamina();
            _playerStats.StaminaIsBeingConsumed = false;
        }

        // Fatiga SFX
        if (!_playerStats.CanSprint && !_heavyBreathingFX.isPlaying)
        {
            _heavyBreathingFX.enabled = true;
        }
        if (_playerStats.CanSprint)
        {
            _heavyBreathingFX.enabled = false;
        }

        // Pasos SFX - animaciones.
        if (isSprinting)
        {
            _animator.SetBool("isMoving", true);
            if (!_footstepsSprintFX.isPlaying)
                _footstepsSprintFX.Play();
            if (_footstepsSFX.isPlaying)
                _footstepsSFX.Stop();
        }
        else if (hasInput)
        {
            _animator.SetBool("isMoving", true);
            if (!_footstepsSFX.isPlaying)
                _footstepsSFX.Play();
            if (_footstepsSprintFX.isPlaying)
                _footstepsSprintFX.Stop();
        }
        else
        {
            _animator.SetBool("isMoving", false);
            if (_footstepsSFX.isPlaying)
                _footstepsSFX.Stop();
            if (_footstepsSprintFX.isPlaying)
                _footstepsSprintFX.Stop();
        }
    }
    public void DisableMovement()
    {
        _canMove = false;
    }
}
