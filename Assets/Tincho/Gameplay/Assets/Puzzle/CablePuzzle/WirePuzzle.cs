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

    public event Action OnPuzzleCompleted;

    public void VerifyVictory()
    {
        if (currentConnections == 4)
        {
            //Destroy(this.gameObject, 1f);
            Debug.Log("You win!");
            winningLight.SetActive(true);
            Destroy(this);

            // 🔥 Disparamos el evento
            OnPuzzleCompleted?.Invoke();

            // Destruimos el script después (después de notificar)
            Destroy(this, 1f);
        }
    }
}
