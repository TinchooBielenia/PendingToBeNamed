using UnityEngine;

public class DrinkablePotion : MonoBehaviour, IInteraction
{
    [SerializeField] private PlayerHealing _player;

    public void TriggerInteraction()
    {
        _player.DrinkPotion();
        Destroy(gameObject);
    }
}
