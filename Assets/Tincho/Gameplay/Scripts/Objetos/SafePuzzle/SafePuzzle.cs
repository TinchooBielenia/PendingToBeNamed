using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SafePuzzle : MonoBehaviour, IInteraction
{
    [SerializeField] private GameObject _canvas;

    private bool _isCanvasVisible = false;

    private bool _isInteracting = false;

    [SerializeField] private string _correctCode = "1234";

    [SerializeField] private TextMeshProUGUI _displayText;

    [SerializeField] private AudioSource _buttonBeepSFX;
    [SerializeField] private AudioSource _openedSafeSFX;
    [SerializeField] private AudioSource _puzzleSolvedSFX;

    private float _delayCount;

    private Animator _animator;

    private string _currentInput = "";

    private int _maxCharacters = 3;

    public void TriggerInteraction()
    {
        _isInteracting = true;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _delayCount = 0.55f;
        HideKeypad();
    }

    private void Update()
    {
        if (_isInteracting && !_isCanvasVisible)
        {
            ShowKeypad();

        }

        if (_isCanvasVisible && InputController.Instance.EscapeKey)
        {
            HideKeypad();
        }
    }

    private void ShowKeypad()
    {
        _isCanvasVisible = true;
        _canvas.SetActive(true);
        Time.timeScale = 0f;
        _isInteracting = false;
    }

    private void HideKeypad()
    {
        _canvas.SetActive(false);
        Time.timeScale = 1f;
        _isCanvasVisible = false;
    }

    private void AddDigit(string digit)
    {
        if (_currentInput.Length > _maxCharacters) return;

        _currentInput += digit;
        _displayText.text = _currentInput;
        _buttonBeepSFX.Play();
    }

    private void ClearInput()
    {
        _currentInput = "";
        _displayText.text = _currentInput;
    }

    private void ConfirmInput()
    {
        if (_currentInput == _correctCode)
        {
            Debug.Log("¡Código correcto!");
            HideKeypad();
            ClearInput();
            gameObject.layer = 0;
            _animator.SetTrigger("puzzleSaved");
            _puzzleSolvedSFX.Play();
            StartCoroutine(OpenSafeSFX());
        }
        else
        {
            Debug.Log("Código incorrecto.");
            ClearInput();
        }
    }

    private IEnumerator OpenSafeSFX()
    {
        yield return new WaitForSeconds(_delayCount);

        _openedSafeSFX.Play();
    }

}