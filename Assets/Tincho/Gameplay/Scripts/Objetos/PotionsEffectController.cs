using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PotionsEffectController : MonoBehaviour
{
    [SerializeField] private float _duration = 10f;
    [SerializeField] private float _delayLoseEffect = 0.3f;
    [SerializeField] private AudioSource _heartbeatSFX;
    private bool _heartbeatStarted = false;

    private PlayerHealth _playerHealth;
    private float _timer;
    private bool _active = false;
    //[SerializeField] private Image _imageEffect;

    private void Start()
    {
        _playerHealth = Player.Instance.GetComponent<PlayerHealth>();
    }

    public void ActivatePotionEffect()
    {
        _timer = _duration;
        _active = true;
        _heartbeatStarted = false;
        _playerHealth.PotionDrunk = true;
        _playerHealth.DrinkPotion();
        PowerUpEffectsUI.Instance.ShowPowerUpSlots(1);
        //EffectsOnScreen.Instance.EffectOnScreenPulse(_imageEffect);
    }

    private void Update()
    {
        if (!_active) return;

        _timer -= Time.deltaTime;

        if (!_heartbeatStarted && _timer < _duration * _delayLoseEffect)
        {
            _heartbeatStarted = true;
            _heartbeatSFX.Play();
            StartCoroutine(FadeOutSound(_heartbeatSFX, _timer)); // fadeout dura hasta que termine el tiempo restante
        }

        if (_timer <= 0f)
        {
            _active = false;
            _playerHealth.PotionDrunk = false;
            PowerUpEffectsUI.Instance.HidePowerUpSlots(1);
        }
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
