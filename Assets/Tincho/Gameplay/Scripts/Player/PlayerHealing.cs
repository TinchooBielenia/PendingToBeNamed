using UnityEngine;
using UnityEngine.UI;

public class PlayerHealing : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] private float _playerLife;
    [SerializeField] private float _maxPlayerLife;

    [SerializeField] private ParticleSystem _healingSFX;
    [SerializeField] private AudioSource _healingAudioSFX;
    [SerializeField] private Image _lifeBar;
    [SerializeField] private AudioSource _getDamagedSFX;
    [SerializeField] private AudioSource _drinkPotionSFX;
    private Animator _animator;
    private Player _player;
    private bool _potionDrunk;
    [SerializeField] private float _waterDamageTimer;

    public float GetPlayerLife() => _playerLife;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _potionDrunk = false;
        _waterDamageTimer = 0;

    }

    public bool PotionDrunk
    {
        get { return _potionDrunk; }
        set { _potionDrunk = value; }
    }

    public float MaxPlayerLife
    {
        get => _maxPlayerLife;
        set { _maxPlayerLife = value; }
    }


    private void Update()
    {
        ManageLifeBar();
    }

    public void ManageLifeBar()
    {
        _lifeBar.fillAmount = _playerLife / _maxPlayerLife;
    }

    public void TakeDamage(float amount)
    {
        _animator.SetTrigger("isGettingDamage");
        _getDamagedSFX.Play();
        _playerLife -= amount;
        _playerLife = Mathf.Clamp(_playerLife, 0, _maxPlayerLife);
        Debug.Log("Jugador recibió daño. Vida actual: " + _playerLife);

        if (_playerLife <= 0) {
            Destroy(_getDamagedSFX);
            GetComponent<Player>().Die();
        }
    }

    public void TakeDamageFromWater(float amount)
    {
        _playerLife -= Time.deltaTime * amount;
        _waterDamageTimer -= Time.deltaTime;

        if (_waterDamageTimer <= 0f)
        {
            _getDamagedSFX.Play();
            _animator.SetTrigger("isGettingDamage");
            _waterDamageTimer = 1f;
        }

        if (_playerLife <= 0)
        {
            Destroy(_getDamagedSFX);
            GetComponent<Player>().Die();
        }
    }

    public void DrinkPotion()
    {
        _animator.SetTrigger("isDrinkingPotion");
        _player.FrozenPlayer();
        _drinkPotionSFX.Play();
        _potionDrunk = true;

        _healingSFX.Play();
        if (!_healingAudioSFX.isPlaying)
            _healingAudioSFX.Play();

        Invoke(nameof(ReturnPlayerControl), 5f);
    }

    private void ReturnPlayerControl()
    {
        _player.UnfreezePlayer();

        _healingSFX.Stop();
        if (_healingAudioSFX.isPlaying)
            _healingAudioSFX.Stop();
    }

    public void HealPlayer(float amount)
    {
        _playerLife += amount;
    }

    public void HealPlayerFromWater(float amount)
    {
        _playerLife += Time.deltaTime * amount;
        _playerLife = Mathf.Clamp(_playerLife, 0, _maxPlayerLife);

        _healingSFX.Play();
        if (!_healingAudioSFX.isPlaying)
            _healingAudioSFX.Play();
    }

    public void StopWaterEffects()
    {
        if (_healingSFX.isPlaying)
            _healingSFX.Stop();

        if (_healingAudioSFX.isPlaying)
            _healingAudioSFX.Stop();

        _animator.ResetTrigger("isGettingDamage");
    }

}