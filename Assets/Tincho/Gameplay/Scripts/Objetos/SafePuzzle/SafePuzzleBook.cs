using System;
using System.Collections.Generic;
using UnityEngine;

public class SafePuzzleBook : MonoBehaviour, IInteraction
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private List<Card> _photosList;
    [SerializeField] private SafePuzzle _safePuzzleCorrectCode;
    private bool _isInteracting;
    private bool _photosShuffled = false;
    private bool _isCanvasVisible;
    [SerializeField] private string _currentCorrectCode;
    [SerializeField] private AudioSource _openBookSFX;

    [SerializeField] private int _hintID = -1;
    [SerializeField] private bool _triggerHintOnOpen = true;
    private bool _wasNoteShown = false;

    private void Start()
    {
        _canvas.SetActive(false);
        _photosShuffled = false;
    }

    [Serializable] private struct Card
    {
        public int value; 
        public GameObject photo;
    }

    private void Update()
    {
        if (_isInteracting && !_isCanvasVisible)
        {
            ShowBook();
            ShufflePhotos();
        }

        if (_isCanvasVisible && InputController.Instance.EscapeKey)
        {
            HideBook();
            _photosShuffled = false;
        }
    }

    private void ShowBook()
    {
        _isCanvasVisible = true;
        _canvas.SetActive(true);
        _openBookSFX.Play();
        Time.timeScale = 0f;
        _isInteracting = false;

        _wasNoteShown = true;
    }

    private void HideBook()
    {
        _canvas.SetActive(false);
        Time.timeScale = 1f;
        _isCanvasVisible = false;

        if (_wasNoteShown && _triggerHintOnOpen && _hintID >= 0)
        {
            HintController.Instance.ShowHint(_hintID, NotesTextContainer.NoteType.Hint);
            _wasNoteShown = false; // Evitar mostrarlo de nuevo por error
        }
    }

    public void TriggerInteraction()
    {
        _isInteracting = true;
    }

    private void ShufflePhotos()
    {
        if (!_photosShuffled)
        {
            for (int i = 0; i < _photosList.Count; i++)
            {
                _currentCorrectCode = "";
                int randomIndex = UnityEngine.Random.Range(i, _photosList.Count);
                Card temp = _photosList[i];
                _photosList[i] = _photosList[randomIndex];
                _photosList[randomIndex] = temp;
            }

            // Refleja el orden en la jerarquía del canvas
            for (int i = 0; i < _photosList.Count; i++)
            {
                _photosList[i].photo.transform.SetSiblingIndex(i);
            }

            //Armar el string del código correcto
            foreach (Card card in _photosList)
            {
                _currentCorrectCode += card.value.ToString();
            }

            //Pasarlo al SafePuzzle
            if (_safePuzzleCorrectCode != null)
            {
                _safePuzzleCorrectCode.UpdateCorrectCode(_currentCorrectCode);
            }

            _photosShuffled = true;
        }
    }
}
