using UnityEngine;
using UnityEngine.UI;

//TP2 - Martin Bielenia
public class PlayerStaminaStats : MonoBehaviour
{
    [Header("StaminaStats")]
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _currentStamina;
    [SerializeField] private float _staminaUseRate = 10f;
    [SerializeField] private float _staminaRegenRate = 5f;

    [Header("Timers")]
    [SerializeField] private float _timerRecoverBase = 10f;
    [SerializeField] private float _timerRecoverDelay;
    [SerializeField] private float _timerEarlyRecoverBase = 3f;
    [SerializeField] private float _timerEarlyRecover;

    [SerializeField] private Image _staminaBar;

    private bool _staminaIsBeingConsumed;
    private bool _canSprint = true;


    public bool CanSprint => _canSprint;

    public bool StaminaIsBeingConsumed { set => _staminaIsBeingConsumed = value;}

    void Start()
    {
        _currentStamina = _maxStamina;
        _timerRecoverDelay = _timerRecoverBase;
        _timerEarlyRecover = _timerEarlyRecoverBase;
        _staminaIsBeingConsumed = false;
    }

    private void Update()
    {
        HandleStaminaTimers();
        RecoverStamina();
        UpdateStaminaBar();
    }

    public void UseStamina()
    {
        _currentStamina -= _staminaUseRate * Time.deltaTime;
        _currentStamina = Mathf.Clamp(_currentStamina, 0f, _maxStamina);
    }

    private void HandleStaminaTimers()
    {
        // Si no hay stamina, comienza delay
        if (_currentStamina <= 0f)
        {
            _timerRecoverDelay -= Time.deltaTime;
            _canSprint = false;
        }

        // Si está completamente recuperado, resetea delay
        if (_currentStamina >= _maxStamina)
        {
            _timerRecoverDelay = _timerRecoverBase;
            _canSprint = true;
        }

        // Si se puede recuperar antes
        if (!_staminaIsBeingConsumed && _currentStamina > 0f && _currentStamina < _maxStamina)
        {
            _timerEarlyRecover -= Time.deltaTime;
        }
        else
        {
            _timerEarlyRecover = _timerEarlyRecoverBase;
        }
    }

    public void RecoverStamina()
    {
        if (_currentStamina < _maxStamina)
        {
            if (_currentStamina <= 0f && _timerRecoverDelay <= 0f)
            {
                AddStamina();
            }
            else if (_currentStamina > 0f && _timerEarlyRecover <= 0f)
            {
                AddStamina();
            }
        }
    }

    private void AddStamina()
    {
        _currentStamina += _staminaRegenRate * Time.deltaTime;
        _currentStamina = Mathf.Clamp(_currentStamina, 0f, _maxStamina);
    }

    private void UpdateStaminaBar()
    {
        if (_staminaBar != null)
        {
            _staminaBar.fillAmount = _currentStamina / _maxStamina;
        }
    }
}
