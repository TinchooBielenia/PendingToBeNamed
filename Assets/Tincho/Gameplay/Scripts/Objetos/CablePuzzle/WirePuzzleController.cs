using System;
using System.Collections.Generic;
using UnityEngine;

//TP2 - Martin Bielenia
public class WirePuzzleController : MonoBehaviour
{
    public static WirePuzzleController Instance { get; private set; }

    [SerializeField] private int _currentConnections;
    [SerializeField] private GameObject _winningLight;
    [SerializeField] private AudioSource _connectionSFX;
    [SerializeField] private AudioSource _wrongSFX;
    [SerializeField] private List<GameObject> _holes;
    [SerializeField] private List<int> _correctOrder;
    [SerializeField] private int _wrongTries;

    public List<GameObject> Holes => _holes;
    public List<int> CorrectOrder => _correctOrder;

    private void Start()
    {
        _wrongTries = 0;
    }

    public event Action OnPuzzleFailed;

    public void AddIncorrect()
    {
        _wrongSFX.Play();
        _wrongTries++;
        VerifyLose();
    }

    private void VerifyLose()
    {
        if (_wrongTries >= 3)
        {
            Debug.Log("You lose!");
            OnPuzzleFailed?.Invoke();
            _wrongTries = 0;
        }
    }

    public event Action OnPuzzleCompleted;

    public void AddCorrect()
    {
        _connectionSFX.Play();
        _currentConnections++;
        VerifyVictory();
    }

    private void VerifyVictory()
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
