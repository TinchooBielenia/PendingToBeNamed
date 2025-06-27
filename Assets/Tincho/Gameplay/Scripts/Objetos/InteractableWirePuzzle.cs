using UnityEngine;

public class InteractableWirePuzzle : MonoBehaviour, IInteraction
{
    private bool _isInteracting = false;
    [SerializeField] private GameObject _wirePuzzlePrefab;
    private GameObject _wirePuzzleInstance;
    [SerializeField] private WirePuzzleController _wirePuzzleClass;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _hud;
    private bool _startVictoryTimer = false;
    [SerializeField] private float _victoryTimer;
    [SerializeField] private AudioSource _electricGeneratorSFX;
    private bool _puzzleFailed;
    [SerializeField] private FocusCamera _camera;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private int _spawnerAmount;


    private void Update()
    {
        if (_startVictoryTimer)
        {
            _victoryTimer -= Time.deltaTime;

            if (_victoryTimer <= 0f)
            {
                ClosePuzzleVictory();
            }
        }
        else if (_puzzleFailed)
        {
            ClosePuzzleFailed();
        }

        if (InputController.Instance.EscapeKey)
        {
            ClosePuzzleOnDemand();
        }
    }

    public void TriggerInteraction()
    {
        _isInteracting = true;
        OpenPuzzle();
    }

    private void OpenPuzzle()
    {
        if (_isInteracting)
        {
            _wirePuzzleInstance = Instantiate(_wirePuzzlePrefab, transform.position, Quaternion.identity);

            _player.SetActive(false);
            _hud.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            _puzzleFailed = false;

            _wirePuzzleClass = _wirePuzzleInstance.GetComponentInChildren<WirePuzzleController>();
            if (_wirePuzzleClass != null)
            {
                _wirePuzzleClass.OnPuzzleCompleted += Victory;
                _wirePuzzleClass.OnPuzzleFailed += PuzzleFailed;
            }
        }
    }

    private void Victory()
    {
        _startVictoryTimer = true;
        _electricGeneratorSFX.Play();
        Invoke(nameof(ShowCamera),1f);
    }

    private void ShowCamera() => _camera.ShowVictoryCamera();
    
    private void PuzzleFailed() => _puzzleFailed = true;
    
    private void ClosePuzzleVictory()
    {
        if (_wirePuzzleInstance != null)
        {
            Destroy(_wirePuzzleInstance);
            _wirePuzzleInstance = null;
        }
        _player.SetActive(true);
        _hud.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.layer = 0;
        _startVictoryTimer = false;
        _spawner.StartSpawning(_spawnerAmount);

        if (_wirePuzzleClass != null)
        {
            _wirePuzzleClass.OnPuzzleCompleted -= Victory;
            _wirePuzzleClass.OnPuzzleFailed -= PuzzleFailed;
            _wirePuzzleClass = null;
        }
    }

    private void ClosePuzzleFailed()
    {
        if (_wirePuzzleInstance != null)
        {
            Destroy(_wirePuzzleInstance);
            _wirePuzzleInstance = null;
        }

        _player.SetActive(true);
        _hud.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ClosePuzzleOnDemand()
    {
        if (_wirePuzzleInstance != null)
        {
            Destroy(_wirePuzzleInstance);
            _wirePuzzleInstance = null;
        }

        _player.SetActive(true);
        _hud.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
