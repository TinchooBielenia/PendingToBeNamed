using UnityEngine;

public class FocusController : MonoBehaviour
{
    [SerializeField] private WirePuzzleController _wirePuzzleClass;

    private bool _focusStarted = false;

    [SerializeField] private float _destroyTimer;

    [SerializeField] private Camera _focusCamera;


    private void Awake()
    {
        Debug.Log("FocusController Awake: Subscribing to event");
        if (_wirePuzzleClass != null)
        {
            _wirePuzzleClass.OnPuzzleCompleted += StartFocus;
        }
    }

    private void Start()
    {
        if (_focusCamera != null)
            _focusCamera.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_focusStarted)
        {
            _destroyTimer -= Time.deltaTime;

            if (_destroyTimer <= 0)
            {
                EndFocus();
            }
        }
    }

    private void StartFocus()
    {
        Debug.Log("StartFocus triggered");
        if (!_focusStarted)
        {
            _focusStarted = true;
            if (_focusCamera != null)
                _focusCamera.gameObject.SetActive(true);
        }
    }

    private void EndFocus()
    {
        Destroy(gameObject);
    }
}
