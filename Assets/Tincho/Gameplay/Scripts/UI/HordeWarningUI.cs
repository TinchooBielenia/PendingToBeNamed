using UnityEngine;
using System.Collections;
using TMPro;

public class HordeWarningUI : MonoBehaviour
{
    public static HordeWarningUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _warningMessage;  
    [SerializeField] private float _duration = 3f;
    [SerializeField] private AudioSource _dangerAlarm;
    [SerializeField] private float _pulseSpeed = 2f;
    private Vector3 _originalScale;
    private bool _isPulsing;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (_warningMessage != null)
            _warningMessage.enabled = false;
    }

    private void Update()
    {
        if (_isPulsing)
        {
            _warningMessage.alpha = Mathf.PingPong(Time.unscaledTime * _pulseSpeed, 1f);
        }
    }

    public void ShowWarning()
    {
        StopAllCoroutines(); 
        StartCoroutine(ShowWarningRoutine());
    }

    private IEnumerator ShowWarningRoutine()
    {
        _warningMessage.enabled = true;
        _isPulsing = true;
        _dangerAlarm.Play();

        yield return new WaitForSecondsRealtime(_duration);

        _isPulsing = false;
        _warningMessage.enabled = false;
    }
}
