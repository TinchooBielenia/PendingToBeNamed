using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EffectsOnScreen : MonoBehaviour
{
    public static EffectsOnScreen Instance;

    [SerializeField] private float _pulseSpeed = 2f;
    [SerializeField] private float _pulseDuration = 1f;
    [SerializeField] private Image effectOnScreen;
    [SerializeField][Range(0f, 1f)] private float _maxAlpha = 0.4f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (effectOnScreen != null)
            effectOnScreen.enabled = false;
    }

    public void EffectOnScreenPulse()
    {
        if (!gameObject.activeInHierarchy) return;

        StopAllCoroutines(); // evita superposición si se llama varias veces seguidas
        StartCoroutine(PulseEffectRoutine());
    }

    private IEnumerator PulseEffectRoutine()
    {
        effectOnScreen.enabled = true;

        float time = 0f;
        Color originalColor = effectOnScreen.color;

        while (time < _pulseDuration)
        {
            float alpha = Mathf.PingPong(Time.unscaledTime * _pulseSpeed, _maxAlpha);
            effectOnScreen.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        // Ocultar al terminar
        effectOnScreen.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        effectOnScreen.enabled = false;
    }

}
