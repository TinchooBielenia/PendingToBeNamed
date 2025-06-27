using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHanlder : MonoBehaviour
{
    public static SceneHanlder Instance;
    [SerializeField] private GameObject _deathCanvas;
    [SerializeField] private GameObject _victoryCanvas;
    [SerializeField] private float _resetDelay;
    private string _currentScene;
    [SerializeField] private string _mainMenuScene;

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
    }

    public void OnPlayerDeath()
    {
        _deathCanvas.SetActive(true);
        StartCoroutine(ResetScene(_currentScene));
    }

    public void OnPlayerVictory()
    {
        _victoryCanvas.SetActive(true);
        StartCoroutine(ResetScene(_mainMenuScene));
    }

    private IEnumerator ResetScene(string desiredScene)
    {
        yield return new WaitForSeconds(_resetDelay);

        SceneManager.LoadScene(desiredScene);
    }
}
