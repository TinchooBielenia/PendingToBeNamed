using UnityEngine;

//TP2 - Martin Bielenia
public class MainDoorLightController : MonoBehaviour
{
    [SerializeField] private GameObject _doorLight;
    [SerializeField] private WirePuzzleController _wirePuzzleClass;

    private void Start()
    {
        _doorLight.SetActive(false);
    }

    private void Update()
    {
        if (WirePuzzleController.Instance != null)
        {
           WirePuzzleController.Instance.OnPuzzleCompleted += TurnOnLight;
        }
    }
    

    private void TurnOnLight()
    {
        _doorLight.SetActive(true);
    }
}
