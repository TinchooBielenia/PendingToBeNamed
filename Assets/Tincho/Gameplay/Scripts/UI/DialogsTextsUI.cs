using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogsTextsUI : MonoBehaviour
{
    public static DialogsTextsUI Instance;

    [SerializeField] private GameObject _bgPanel;
    [SerializeField] private List<TextMeshProUGUI> _texts;
    [SerializeField] private float _routineDuration;
    [SerializeField] private AudioSource _dialogSFX;
    private bool _dialogIsOpened;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _bgPanel.SetActive(false);
        foreach (TextMeshProUGUI slot in _texts)
        {
            slot.enabled = false;
        }

        _dialogIsOpened = false;
    }

    public void ShowDialogTexts(int value)
    {
        if (value <= 0 || value > _texts.Count)
        {
            Debug.LogWarning("Índice fuera de rango en ShowPowerUpSlots");
            return;
        }

        if (!_dialogIsOpened)
        {
            _bgPanel.SetActive(true);
            _dialogSFX.Play();
            TextMeshProUGUI targetSlot = _texts[value - 1];
            targetSlot.enabled = true;
            _dialogIsOpened = true;
            StartCoroutine(HideTextsAfterDelay(targetSlot));
        }
        
    }

    private IEnumerator HideTextsAfterDelay(TextMeshProUGUI text)
    {
        yield return new WaitForSecondsRealtime(_routineDuration);
        _bgPanel.SetActive(false);
        text.enabled = false;
        _dialogIsOpened = false;
    }
}
