using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHanlder : MonoBehaviour
{
    public static SceneHanlder Instance;
    [SerializeField] private GameObject _deathCanvas;
    [SerializeField] private GameObject _endSceneCanvas;
    [SerializeField] private float _resetDelay;
    [SerializeField] private TextMeshProUGUI _resetCounterText;
    private string _currentScene;
    [SerializeField] private string _mainMenuScene;
    [SerializeField] private string _endScene = "04_EndScene";
    [SerializeField] private Player _player;

    private HintController _firstHint;
    [SerializeField] private int _hintID = 100;

    private AsyncOperation _loadOp;

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
        _endSceneCanvas.SetActive(false);

        StartCoroutine(InitFirstHint());
    }

    public void OnPlayerDeath()
    {
        _deathCanvas.SetActive(true);
        StartCoroutine(ResetScene(_currentScene));
    }

    public void StartPreloadEndScene()
    {
        _loadOp = SceneManager.LoadSceneAsync(_endScene);
        _loadOp.allowSceneActivation = false;
    }

    public void ActivateEndScene()
    {
        StartCoroutine(ActivateEndSceneDelay());
    }

    private IEnumerator ActivateEndSceneDelay()
    {
        _endSceneCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        _loadOp.allowSceneActivation = true;
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

    private IEnumerator InitFirstHint()
    {
        yield return new WaitForSecondsRealtime(5);
        _firstHint = HintController.Instance;

        if (_hintID >= 0)
        {
            HintController.Instance.ShowHint(_hintID, NotesTextContainer.NoteType.Hint);
        }
    }
}
