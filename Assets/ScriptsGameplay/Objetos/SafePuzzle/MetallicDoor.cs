using System;
using UnityEngine;

//TP2 - Martin Bielenia
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
        if (Inventory.instance.HasItem("Generator Key"))
        {
            _isInteracting = true;
        }
        else
        {
            _isInteracting= false;
        }
        
    }

    private void Update()
    {
        if (_isInteracting && Inventory.instance.HasItem("Generator Key"))
        {
            OpenMetallicDoor();
        }
    }

    private void OpenMetallicDoor()
    {
        _openedDoorSFX.Play();
        _metallicDoorAnimation.SetTrigger("doorOpened");
        Inventory.instance.DeletePickedItemFromInventory("Generator Key");
        _isInteracting = false;
        gameObject.layer = 0;
    }
}