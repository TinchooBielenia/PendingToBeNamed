using System;
using System.Collections;
using TMPro;
using UnityEngine;

//TP2 - Martin Bielenia
public class SafePuzzle : MonoBehaviour, IInteraction
{
    [SerializeField] private GameObject _canvas;

    private bool _isCanvasVisible = false;

    private bool _isInteracting = false;

    [SerializeField] private string _correctCode;

    [SerializeField] private TextMeshProUGUI _displayText;

    [SerializeField] private AudioSource _buttonBeepSFX;
    [SerializeField] private AudioSource _openedSafeSFX;
    [SerializeField] private AudioSource _puzzleSolvedSFX;
    [SerializeField] private AudioSource _wrongCodeSFX;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private int _spawnerAmount;

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

        if (_isCanvasVisible && InputController.Instance.IsEscape)
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
            _spawner.StartSpawning(_spawnerAmount);
        }
        else
        {
            Debug.Log("Código incorrecto.");
            _wrongCodeSFX.Play();
            ClearInput();
        }
    }

    private IEnumerator OpenSafeSFX()
    {
        yield return new WaitForSeconds(_delayCount);

        _openedSafeSFX.Play();
    }

    public void UpdateCorrectCode(string code)
    {
        _correctCode = code;
    }

}