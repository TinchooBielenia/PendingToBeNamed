using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        // Setear alpha visible antes del pulso
        Color color = targetSlot.color;
        color.a = _maxAlpha;
        targetSlot.color = color;

        StartCoroutine(PulseEffectRoutine(targetSlot));
    }

    private IEnumerator PulseEffectRoutine(Image effectImage)
    {
        float time = 0f;
        Color baseColor = effectImage.color;
        baseColor.a = _maxAlpha;

        while (time < _pulseDuration)
        {
            float alpha = Mathf.PingPong(Time.unscaledTime * _pulseSpeed, _maxAlpha);
            effectImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        // Al terminar, dejar alpha visible
        effectImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, _maxAlpha);
    }

    public void HidePowerUpSlots(int value)
    {
        if (value <= 0 || value > _slots.Count)
        {
            Debug.LogWarning("Índice fuera de rango en HidePowerUpSlots");
            return;
        }

        Image targetSlot = _slots[value - 1];
        // Apagar directo sin corrutina
        targetSlot.enabled = false;
    }

}
