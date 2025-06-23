using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private AudioMixer _mainGameAudioMixer;
    [SerializeField] private string _volumeParameter = "Master"; // debe coincidir con el nombre en el Mixer

    private void Start()
    {
        // Cargar valor actual del volumen en el Slider
        float currentVolume;
        if (_mainGameAudioMixer.GetFloat(_volumeParameter, out currentVolume))
        {
            // Convertimos de decibeles a [0,1] para el slider (si es lineal)
            _volumeSlider.value = Mathf.Pow(10f, currentVolume / 20f);
        }

        // Listener del slider
        _volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float sliderValue)
    {
        // Convertimos de [0,1] (lineal) a decibeles
        float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        _mainGameAudioMixer.SetFloat(_volumeParameter, dB);
    }
}
