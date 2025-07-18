using System;
using TMPro;
using UnityEngine;

//TP2 - Juliana Dimeglio
public class NoteController : MonoBehaviour, IInteraction
{
    [SerializeField] private GameObject _canvas;
    private bool _isInteracting = false;
    private bool _isCanvasVisible = false;
    [SerializeField] private TextMeshProUGUI _noteValue;
    [SerializeField] private int _noteID;
    [SerializeField] private NotesTextContainer.NoteType _textType;
    [SerializeField] private AudioSource _openCloseNoteSFX;

    public delegate void OnNoteOpened(int noteID);
    public static event OnNoteOpened NoteOpened;

    private void Start()
    {
        HideNote();
    }

    private void Update()
    {
        if (_isInteracting && !_isCanvasVisible)
        {
            ShowNote();

        }

        if (_isCanvasVisible && InputController.Instance.EscapeKey)
        {
            HideNote();
            _openCloseNoteSFX.Play();
        }
    }

    public void TriggerInteraction()
    {
        _isInteracting = true;
    }

    private void ShowNote()
    {
        _isCanvasVisible = true;
        _canvas.SetActive(true);
        Time.timeScale = 0f;
        _isInteracting = false;
        _noteValue.text = NotesTextContainer.GetTextByID(_noteID, _textType);
        _openCloseNoteSFX.Play();
        NoteOpened?.Invoke(_noteID);
    }

    private void HideNote()
    {
        _canvas.SetActive(false);
        Time.timeScale = 1f;
        _isCanvasVisible = false;
    }
}
