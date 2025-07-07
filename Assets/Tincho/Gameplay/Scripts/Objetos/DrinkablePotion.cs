using UnityEngine;

public class DrinkablePotion : MonoBehaviour, IInteraction
{
    [SerializeField] private PlayerHealth _player;

    public void TriggerInteraction()
    {
        _player.DrinkPotion();
        Destroy(gameObject);
    }
}
