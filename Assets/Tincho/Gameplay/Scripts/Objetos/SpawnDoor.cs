using UnityEngine;

public class SpawnDoor : MonoBehaviour, IInteraction
{
    private Animator _spawnDoorAnimation;
    //[SerializeField] private AudioSource _openedDoorSFX;
    [SerializeField] private float _closeTimer;
    private bool _isOpen = false;
    private bool _doorInteracted = false;   

    private void Start()
    {
        _spawnDoorAnimation = GetComponent<Animator>();
    }

    public void TriggerInteraction()
    {
        if (_isOpen)
        {
            CloseSpawnDoor();
        }
        else
        {
            OpenSpawnDoor();
        }
    }

    private void Update()
    {
        if (_doorInteracted)
        {
            _doorInteracted = false;
            OpenSpawnDoor();

            if (_doorInteracted)
            {
                CloseSpawnDoor();
                _doorInteracted = false;
            }
        }
    }

    private void OpenSpawnDoor()
    {
        _isOpen = true;
        _spawnDoorAnimation.SetBool("openDoor", true);
        // Si querés que se cierre sola después de un tiempo:
        //if (_closeTimer > 0)
        //{
        //    Invoke(nameof(CloseSpawnDoor), _closeTimer);
        //}
    }

    private void CloseSpawnDoor()
    {
        _isOpen = false;
        _spawnDoorAnimation.SetBool("openDoor", false);
    }
}
