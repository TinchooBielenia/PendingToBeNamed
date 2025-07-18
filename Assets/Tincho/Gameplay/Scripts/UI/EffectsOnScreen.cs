using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EffectsOnScreen : MonoBehaviour
{
    public static EffectsOnScreen Instance;

    [SerializeField] private float _pulseSpeed = 2f;
    [SerializeField] private float _pulseDuration = 1f;
    private Image _effectOnScreen;
    [SerializeField][Range(0f, 1f)] private float _maxAlpha = 0.4f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (_effectOnScreen != null)
            _effectOnScreen.enabled = false;
    }

    public void EffectOnScreenPulse(Image imageEffect)
    {
        if (!gameObject.activeInHierarchy) return;

        _effectOnScreen = imageEffect;

        StopAllCoroutines(); // evita superposición si se llama varias veces seguidas
        StartCoroutine(PulseEffectRoutine());
    }

    private IEnumerator PulseEffectRoutine()
    {
        _effectOnScreen.enabled = true;

        float time = 0f;
        Color originalColor = _effectOnScreen.color;

        while (time < _pulseDuration)
        {
            float alpha = Mathf.PingPong(Time.unscaledTime * _pulseSpeed, _maxAlpha);
            _effectOnScreen.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        // Ocultar al terminar
        _effectOnScreen.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        _effectOnScreen.enabled = false;
    }

}
