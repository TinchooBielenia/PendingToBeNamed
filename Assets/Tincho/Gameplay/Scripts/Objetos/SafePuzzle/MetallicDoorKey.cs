using UnityEngine;

public class MetallicDoorKey : MonoBehaviour, IInteraction
{
    private bool _isInteracting;
    private bool _playerHasKey;
    [SerializeField] private bool _playerIsInDoorRange;
    [SerializeField] private Animator _metallicDoorAnimation;

    private void Start()
    {
        _isInteracting = false;
        _playerHasKey = false;
    }

    private void Update()
    {
        if (_isInteracting)
        {
            TakeKey();
        }

        if (_playerIsInDoorRange && _isInteracting && _playerHasKey)
        {
            OpenMetallicDoor();
        }
    }

    public void TriggerInteraction()
    {
        _isInteracting = true;
    }

    private void TakeKey()
    {
        _playerHasKey = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MetallicCableDoor"))
        {
            _metallicDoorAnimation = other.GetComponent<Animator>();
            _playerIsInDoorRange = true;
        }
    }

    private void OpenMetallicDoor()
    {
        _metallicDoorAnimation.SetTrigger("doorOpened");
    }
}
