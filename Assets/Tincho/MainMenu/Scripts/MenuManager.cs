using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [Header("Botones del menú")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _backButton;

    [Header("Sonido botones")]
    [SerializeField] private AudioSource _playButtonSFX;

    [Header("Escenas")]
    [SerializeField] private string _gameplaySceneName = "02_Gameplay"; 

    [Header("Imagenes")]
    [SerializeField] private RawImage _creditsImage;


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
        // Asignar listeners a los botones
        if (_playButton != null)
            _playButton.onClick.AddListener(PlayGame);

        if (_creditsButton != null)
            _creditsButton.onClick.AddListener(OpenCredits);

        if (_backButton != null)
            _backButton.onClick.AddListener(BackButton);

        if (_quitButton != null)
            _quitButton.onClick.AddListener(QuitGame);

        _creditsImage.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);
    }

    private void PlayGame()
    {
        Debug.Log("Iniciando juego...");
        SceneManager.LoadScene(_gameplaySceneName);
    }

    private void OpenCredits()
    {
        Debug.Log("Abriendo creditos...");
        _creditsImage.gameObject.SetActive(true);
        _backButton.gameObject.SetActive(true);
    }

    private void BackButton()
    {
        _creditsImage.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);
    }

    private void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

