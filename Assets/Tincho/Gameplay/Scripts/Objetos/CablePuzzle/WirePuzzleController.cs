using System;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleController : MonoBehaviour
{
    public int currentConnections;
    public GameObject winningLight;
    public GameObject errorLight;
    public AudioSource connectionSFX;
    public AudioSource wrongSFX;
    public List<GameObject> Holes;
    public List<int> correctOrder;
    public int wrongTries;

    private void Start()
    {
        wrongTries = 0;
    }

    public event Action OnPuzzleFailed;

    public void VerifyLose()
    {
        if (wrongTries >= 3)
        {
            Debug.Log("You lose!");
            OnPuzzleFailed?.Invoke();
            wrongTries = 0;
        }
    }

    public event Action OnPuzzleCompleted;

    public void VerifyVictory()
    {
        if (currentConnections == 4)
        {
            Debug.Log("You win!");
            winningLight.SetActive(true);
            Destroy(this);

            OnPuzzleCompleted?.Invoke();

            Destroy(this, 1f);
        }
    }
}
