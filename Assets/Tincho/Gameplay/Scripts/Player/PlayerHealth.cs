using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] private float _playerLife;
    [SerializeField] private float _maxPlayerLife;

    [SerializeField] private ParticleSystem _healingSFX;
    [SerializeField] private AudioSource _healingAudioSFX;
    [SerializeField] private Image _lifeBar;
    [SerializeField] private AudioSource _getDamagedSFX;
    [SerializeField] private AudioSource _drinkPotionSFX;
    [SerializeField] private AudioSource _medkitSFX;
    [SerializeField] private Image _imageEffect;
    private Animator _animator;
    private Player _player;
    private bool _potionDrunk;
    private bool _playerIsDead = false;
    [SerializeField] private float _waterDamageTimer;

    public float PlayerLife() => _playerLife;

    public bool PlayerDeath() => _playerIsDead;

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
        EffectsOnScreen.Instance.EffectOnScreenPulse(_imageEffect);
        Debug.Log("Jugador recibió daño. Vida actual: " + _playerLife);

        if (_playerLife <= 0) {
            Destroy(_getDamagedSFX);
            GetComponent<Player>().Die();
            _playerIsDead = true;
        }
    }

    public void TakeDamageFromWater(float amount)
    {
        _playerLife -= Time.deltaTime * amount;
        _waterDamageTimer -= Time.deltaTime;

        if (_waterDamageTimer <= 0f)
        {
            _getDamagedSFX.Play();
            EffectsOnScreen.Instance.EffectOnScreenPulse(_imageEffect);
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
        _drinkPotionSFX.Play();

        StartHealingEffects();

        Invoke(nameof(StopHealingEffects), 4f);
    }

    public void HealPlayer(float amount)
    {
        _playerLife += amount;
        _medkitSFX.Play();
    }

    public void HealPlayerFromWater(float amount)
    {
        _playerLife += Time.deltaTime * amount;
        _playerLife = Mathf.Clamp(_playerLife, 0, _maxPlayerLife);
    }

    public void StartHealingEffects()
    {
        _healingSFX.Play();
        _healingAudioSFX.Play();
    }

    public void StopHealingEffects()
    {
        if (_healingSFX.isPlaying)
            _healingSFX.Stop();

        if (_healingAudioSFX.isPlaying)
            StartCoroutine(FadeOutSound(_healingAudioSFX, 1f));

        _animator.ResetTrigger("isGettingDamage");
    }

    private IEnumerator FadeOutSound(AudioSource audioSource, float fadeDuration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Restaurar volumen original por si se vuelve a usar
    }

}