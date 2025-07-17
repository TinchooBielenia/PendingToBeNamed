using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneHanlder : MonoBehaviour
{
    public static SceneHanlder Instance;
    [SerializeField] private GameObject _deathCanvas;
    [SerializeField] private GameObject _victoryCanvas;
    [SerializeField] private float _resetDelay;
    [SerializeField] private TextMeshProUGUI _resetCounterText;
    private string _currentScene;
    [SerializeField] private string _mainMenuScene;
    [SerializeField] private Player _player;
    [SerializeField] private AudioSource _finishGameSongSFX;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene().name;
        _victoryCanvas.SetActive(false);
    }

    public void OnPlayerDeath()
    {
        _deathCanvas.SetActive(true);
        StartCoroutine(ResetScene(_currentScene));
    }

    public void OnPlayerVictory()
    {
        _victoryCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        _player.FrozenPlayer();
        _finishGameSongSFX.Play();
    }

    private IEnumerator ResetScene(string desiredScene)
    {
        float timer = _resetDelay;
        while (timer > 0)
        {
            _resetCounterText.SetText(((int)Mathf.Ceil(timer)).ToString());
            timer -= Time.unscaledDeltaTime;
            yield return null;
        }

        SceneManager.LoadScene(desiredScene);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }

    public void TravelToTestRoom(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
