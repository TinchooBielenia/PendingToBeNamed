using TMPro;
using UnityEngine;

public class SafePuzzle : MonoBehaviour, IInteraction
{
    [SerializeField] private GameObject canvas;

    private bool _isCanvasVisible = false;

    private bool _isInteracting = false;

    [SerializeField] private string _correctCode = "1234";

    [SerializeField] private TextMeshProUGUI _displayText;

    private string _currentInput = "";

    private int _maxCharacters = 3;

    public void TriggerInteraction()
    {
        _isInteracting = true;
    }

    private void Start()
    {
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
        canvas.SetActive(true);
        Time.timeScale = 0f;
        _isInteracting = false;
    }

    private void HideKeypad()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;
        _isCanvasVisible = false;
    }

    private void AddDigit(string digit)
    {
        if (_currentInput.Length > _maxCharacters) return;

        _currentInput += digit;
        _displayText.text = _currentInput;
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
        }
        else
        {
            Debug.Log("Código incorrecto.");
            ClearInput();
        }
    }
}
