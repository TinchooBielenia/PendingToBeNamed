using System;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleController : MonoBehaviour
{
    [SerializeField] int _currentConnections;
    [SerializeField] private GameObject _winningLight;
    [SerializeField] private AudioSource _connectionSFX;
    [SerializeField] private AudioSource _wrongSFX;
    public List<GameObject> holes;
    public List<int> correctOrder;
    [SerializeField] private int _wrongTries;

    public int WrongTries
    {
        get { return _wrongTries; }
        set { _wrongTries = value; }
    }
    public int CurrentConnections
    {
        get { return _currentConnections; }
        set { _currentConnections = value; }
    }
    public AudioSource ConnectionSFX
    {
        get { return _connectionSFX; }
        set { _connectionSFX = value; }
    }
    public AudioSource WrongSFX
    {
        get { return _wrongSFX; }
        set { _wrongSFX = value; }
    }
    private void Start()
    {
        _wrongTries = 0;
    }

    public event Action OnPuzzleFailed;

    public void VerifyLose()
    {
        if (_wrongTries >= 3)
        {
            Debug.Log("You lose!");
            OnPuzzleFailed?.Invoke();
            _wrongTries = 0;
        }
    }

    public event Action OnPuzzleCompleted;

    public void VerifyVictory()
    {
        if (_currentConnections == 4)
        {
            Debug.Log("You win!");
            _winningLight.SetActive(true);
            Destroy(this);

            OnPuzzleCompleted?.Invoke();

            Destroy(this, 1f);
        }
    }
}
