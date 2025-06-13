using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] MiniCameraDisplay _camera;


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

      /*  if (Input.GetKeyDown(KeyCode.F))
        {
            Victory();
        }*/
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
        Invoke(nameof(ShowCamera),4f);
    }

    private void ShowCamera()
    {
        _camera.ShowVictoryCamera();
    }

    private void PuzzleFailed()
    {
        _puzzleFailed = true;
    }

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

        if (_wirePuzzleClass != null)
        {
            _wirePuzzleClass.OnPuzzleCompleted -= Victory;
            _wirePuzzleClass.OnPuzzleFailed -= PuzzleFailed;
            _wirePuzzleClass = null;
        }
        //_camera.SetActive(true);
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
}
