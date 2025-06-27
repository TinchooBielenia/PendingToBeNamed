using System;
using UnityEngine;

public class MetallicDoor : MonoBehaviour, IInteraction
{
    private Animator _metallicDoorAnimation;
    private bool _isInteracting;
    [SerializeField] private AudioSource _openedDoorSFX;

    private void Start()
    {
        _metallicDoorAnimation = GetComponent<Animator>();
    }

    public void TriggerInteraction()
    {
        _isInteracting = true;
    }

    private void Update()
    {
        if (_isInteracting && PlayerItemsPickedUp.instance.HasItem("Generator Key"))
        {
            OpenMetallicDoor();
        }
    }

    private void OpenMetallicDoor()
    {
        _openedDoorSFX.Play();
        _metallicDoorAnimation.SetTrigger("doorOpened");
        PlayerItemsPickedUp.instance.DeletePickedItemFromInventory("Generator Key");
        _isInteracting = false;
        gameObject.layer = 0;
    }
}