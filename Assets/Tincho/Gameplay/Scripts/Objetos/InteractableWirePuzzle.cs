using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWirePuzzle : MonoBehaviour, IInteraction
{
    private bool _isInteracting = false;
    [SerializeField] private GameObject _wirePuzzleCanvas;
    [SerializeField] private WirePuzzleController _wirePuzzleClass;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _hud;
    private bool _startVictoryTimer = false;
    [SerializeField] private float _victoryTimer;
    [SerializeField] private AudioSource _electricGeneratorSFX;

    private void Update()
    {
        if (_startVictoryTimer)
        {
            _victoryTimer -= Time.deltaTime;

            if (_victoryTimer <= 0f)
            {
                ClosePuzzle();
            }
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
            _wirePuzzleCanvas.SetActive(true);
            _player.SetActive(false);
            _hud.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            _wirePuzzleClass = _wirePuzzleClass.GetComponentInChildren<WirePuzzleController>();
            if (_wirePuzzleClass != null)
            {
                _wirePuzzleClass.OnPuzzleCompleted += Victory;
            }
        }
    }

    private void Victory()
    {
        _startVictoryTimer = true;
        _electricGeneratorSFX.Play();
    }

    private void ClosePuzzle()
    {
        _wirePuzzleCanvas.SetActive(false);
        _player.SetActive(true);
        _hud.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.layer = 0;
        _startVictoryTimer = false;

        if (_wirePuzzleClass != null)
        {
            _wirePuzzleClass.OnPuzzleCompleted -= Victory;
        }
    }
}
