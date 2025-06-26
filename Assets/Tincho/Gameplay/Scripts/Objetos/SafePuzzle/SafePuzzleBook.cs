using System;
using System.Collections.Generic;
using UnityEngine;

public class SafePuzzleBook : MonoBehaviour, IInteraction
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private List<Card> _photosList;
    private bool _isInteracting;
    private bool _photosShuffled = false;
    private bool _isCanvasVisible;

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
        Time.timeScale = 0f;
        _isInteracting = false;
    }

    private void HideBook()
    {
        _canvas.SetActive(false);
        Time.timeScale = 1f;
        _isCanvasVisible = false;
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

            _photosShuffled = true;
        }
    }
}
