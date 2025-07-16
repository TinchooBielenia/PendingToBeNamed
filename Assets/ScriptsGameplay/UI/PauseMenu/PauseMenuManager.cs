using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

//TP2 - Martin Bielenia
public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance { get; private set; }

    [Header("Botones del menú de pausa")]
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _xButton;
    [SerializeField] private bool _isPaused;
    [SerializeField] private GameObject _canvas;
    
    [Header("Escenas")]
    [SerializeField] private string _sceneName; 

    [Header("Imagenes")]
    [SerializeField] private RawImage _ControlsImage;


    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _canvas.gameObject.SetActive(false);

        if (_optionsButton != null)
            _optionsButton.onClick.AddListener(OpenOptions);

        if (_mainMenuButton != null)
            _mainMenuButton.onClick.AddListener(MainMenu);

        if (_xButton != null)
            _xButton.onClick.AddListener(BackButton);

        _ControlsImage.gameObject.SetActive(false);
        _xButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (InputController.Instance.IsEscape)
        {
            TogglePause();
        }
    }

    private void OpenOptions()
    {
        Debug.Log("Abriendo creditos...");
        _ControlsImage.gameObject.SetActive(true);
        _xButton.gameObject.SetActive(true);
    }

    private void BackButton()
    {
        _ControlsImage.gameObject.SetActive(false);
        _xButton.gameObject.SetActive(false);
    }

    private void MainMenu()
    {
        Debug.Log("Saliendo del juego...");
        SceneManager.LoadScene(_sceneName);
        Destroy(gameObject);
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;
        _canvas.gameObject.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0f : 1f;
        Cursor.lockState = _isPaused ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = _isPaused;
    }

}

