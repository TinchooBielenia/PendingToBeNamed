using UnityEngine;
using System.Collections;

//TP2 - Juliana Dimeglio
public class HordeWarningUI : MonoBehaviour
{
    public static HordeWarningUI Instance { get; private set; }

    [SerializeField] private GameObject _warningImage;  
    [SerializeField] private float _duration = 3f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (_warningImage != null)
            _warningImage.SetActive(false);
    }

    public void ShowWarning()
    {
        StopAllCoroutines(); 
        StartCoroutine(ShowWarningRoutine());
    }

    private IEnumerator ShowWarningRoutine()
    {
        _warningImage.SetActive(true);
        yield return new WaitForSecondsRealtime(_duration);
        _warningImage.SetActive(false);
    }
}
