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
        if (_isInteracting && Inventory.instance.HasItem("Generator Key"))
        {
            OpenMetallicDoor();
        }
        else if (_isInteracting && !Inventory.instance.HasItem("Generator Key"))
        {
            DialogsTextsUI.Instance.ShowDialogTexts(2);
            _isInteracting = false;
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