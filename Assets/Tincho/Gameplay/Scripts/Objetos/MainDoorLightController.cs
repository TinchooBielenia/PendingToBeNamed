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
        _wirePuzzleClass = FindObjectOfType<WirePuzzleController>();
        if (_wirePuzzleClass != null)
        {
            _wirePuzzleClass.OnPuzzleCompleted += TurnOnLight;
        }
    }
    

    private void TurnOnLight()
    {
        _doorLight.SetActive(true);
    }
}
