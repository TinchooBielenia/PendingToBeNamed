using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro.EditorUtilities;
using TMPro;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [Header("Botones del menú")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _controlsButton;
    [SerializeField] private Button _audioButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _quitButton;

    [Header("Escenas")]
    [SerializeField] private string _gameplaySceneName = "02_Gameplay";

    [Header("Mostrables en pantalla")]
    [SerializeField] private List<GameObject> _imagesDisplayedOnScreen;

    [Header("Misc")]
    [SerializeField] private Image _sidePanel;
    private bool _sidePanelActive = false;
    private GameObject _currentVisible;


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
        _sidePanel.gameObject.SetActive(false);

        foreach (GameObject go in _imagesDisplayedOnScreen)
        {
            go.SetActive(false);
        }

        if (_playButton != null)
            _playButton.onClick.AddListener(PlayGame);

        if (_controlsButton != null)
            _controlsButton.onClick.AddListener(() => ShowOnly(_imagesDisplayedOnScreen[0]));

        if (_audioButton != null)
            _audioButton.onClick.AddListener(() => ShowOnly(_imagesDisplayedOnScreen[2]));

        if (_creditsButton != null)
            _creditsButton.onClick.AddListener(() => ShowOnly(_imagesDisplayedOnScreen[1]));

        if (_quitButton != null)
            _quitButton.onClick.AddListener(QuitGame);
    }

    private void PlayGame()
    {
        Debug.Log("Iniciando juego...");
        SceneManager.LoadScene(_gameplaySceneName);
    }

    private void OpenSidePanel()
    {
        _sidePanel.gameObject.SetActive(true);
        _sidePanelActive = true;
    }

    private void CloseSidePanel()
    {
        _sidePanel.gameObject.SetActive(false);
        _sidePanelActive = false;
    }

    private void ShowOnly(GameObject toShow)
    {
        if (_currentVisible == toShow)
        {
            toShow.SetActive(false);
            CloseSidePanel();
            _currentVisible = null;
            return;
        }

        foreach (GameObject go in _imagesDisplayedOnScreen)
        {
            go.SetActive(false);
        }

        toShow.SetActive(true);
        _currentVisible = toShow;

        if (!_sidePanelActive)
            OpenSidePanel();
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

