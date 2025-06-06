using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealing : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] private float _playerLife;
    [SerializeField] private float _maxPlayerLife;

    [SerializeField] private ParticleSystem _healingSFX;
    [SerializeField] private bool _isHealing = false;
    [SerializeField] private AudioSource _healingAudioSFX;
    [SerializeField] private Image _lifeBar;
    [SerializeField] private AudioSource _getDamagedSFX;
    private Animator _animator;

    public float GetPlayerLife()
    {
        return _playerLife;
    }


    void Start()
    {
        _playerLife = 80;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ManageLifeBar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealingWater") && _playerLife < _maxPlayerLife)
        {
            if (!_isHealing)
            {
                _healingSFX.Play();
                _healingAudioSFX.Play();
                _isHealing = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HealingWater") && _playerLife < _maxPlayerLife)
        {
            _playerLife += Time.deltaTime;
        }
        else if (_playerLife >= _maxPlayerLife)
        {
            _healingSFX.Stop();
            _healingAudioSFX.Stop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HealingWater"))
        {
            _healingSFX.Stop();
            _healingAudioSFX.Stop();
            _isHealing = false;
        }
    }

    public void ManageLifeBar()
    {
        _lifeBar.fillAmount = _playerLife / _maxPlayerLife;
    }
    public void TakeDamage(float amount)
    {
        _getDamagedSFX.Play();
        _playerLife -= amount;
        _playerLife = Mathf.Clamp(_playerLife, 0, _maxPlayerLife);
        Debug.Log("Jugador recibió daño. Vida actual: " + _playerLife);
        if (_playerLife <= 0) {
            GetComponent<Player>().Die();
        }
    }

}
