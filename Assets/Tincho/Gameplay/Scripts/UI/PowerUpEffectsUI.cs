using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PowerUpEffectsUI : MonoBehaviour
{
    public static PowerUpEffectsUI Instance;

    [SerializeField] private List<Image> _slots;
    [SerializeField] private float _pulseSpeed = 2f;
    [SerializeField] private float _pulseDuration = 1f;
    [SerializeField][Range(0f, 1f)] private float _maxAlpha = 0.4f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Desactivá todos los íconos al principio
        foreach (Image slot in _slots)
        {
            slot.enabled = false;
        }
    }

    public void ShowPowerUpSlots(int value)
    {
        if (value <= 0 || value > _slots.Count)
        {
            Debug.LogWarning("Índice fuera de rango en ShowPowerUpSlots");
            return;
        }

        Image targetSlot = _slots[value - 1];
        targetSlot.enabled = true;
        StartCoroutine(PulseEffectRoutine(targetSlot));
    }

    private IEnumerator PulseEffectRoutine(Image effectImage)
    {
        float time = 0f;
        Color originalColor = effectImage.color;

        while (time < _pulseDuration)
        {
            float alpha = Mathf.PingPong(Time.unscaledTime * _pulseSpeed, _maxAlpha);
            effectImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        // Restaurar alpha original
        effectImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, originalColor.a);
    }
}
