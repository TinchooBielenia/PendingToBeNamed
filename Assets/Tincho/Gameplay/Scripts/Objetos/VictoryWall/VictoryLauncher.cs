using UnityEngine;

//TP2 - Martin Bielenia
public class VictoryLauncher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LaunchVictory();
        }
    }

    private void LaunchVictory()
    {
        SceneHanlder.Instance.OnPlayerVictory();
    }
}
