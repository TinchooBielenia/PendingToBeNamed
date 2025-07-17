using UnityEngine;

public class DrinkablePotion : BaseInteractableObject, IInteraction
{
    [SerializeField] private PlayerHealth _playerHealth;

    public void TriggerInteraction()
    {
        _playerHealth.DrinkPotion();
        Destroy(gameObject);
    }
}
