using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealing : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] private float _playerLife;
    [SerializeField] private float _maxPlayerLife;
    [SerializeField] private float _healingRate;

    [SerializeField] private ParticleSystem _healingSFX;
    [SerializeField] private AudioSource _healingAudioSFX;
    [SerializeField] private Image _lifeBar;
    [SerializeField] private AudioSource _getDamagedSFX;
    [SerializeField] private AudioSource _drinkPotionSFX;
    private Animator _animator;
    private Player _player;

    public float GetPlayerLife() => _playerLife;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();   

    }

    private void Update()
    {
        ManageLifeBar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealingWater") && _playerLife < _maxPlayerLife)
        {
            _healingSFX.Play();
            if (!_healingAudioSFX.isPlaying)
                _healingAudioSFX.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HealingWater") && _playerLife < _maxPlayerLife)
        {
            _playerLife += Time.deltaTime * _healingRate;
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
            if (_healingSFX.isPlaying)
                _healingSFX.Stop();

            if (_healingAudioSFX.isPlaying)
                _healingAudioSFX.Stop();
        }
    }

    public void ManageLifeBar()
    {
        _lifeBar.fillAmount = _playerLife / _maxPlayerLife;
    }
    public void TakeDamage(float amount)
    {
        _animator.SetBool("isGettingDamage", true);
        _getDamagedSFX.Play();
        _playerLife -= amount;
        _playerLife = Mathf.Clamp(_playerLife, 0, _maxPlayerLife);
        Debug.Log("Jugador recibió daño. Vida actual: " + _playerLife);

        if (_playerLife <= 0) {
            Destroy(_getDamagedSFX);
            GetComponent<Player>().Die();
        }
    }

    public void DrinkPotion()
    {
        _animator.SetTrigger("isDrinkingPotion");
        _player.FrozenPlayer();
        _drinkPotionSFX.Play();

        Invoke(nameof(ReturnPlayerControl), 5f);
    }

    private void ReturnPlayerControl()
    {
        _player.UnfreezePlayer();
    }

}
