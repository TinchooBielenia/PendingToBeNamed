using UnityEngine;

//TP2 - Martin Bielenia
public class PistolGunLoot : BaseLoot
{
    private WeaponPickup _playerWeaponPickup;

    protected override void OnLootTrigger(GameObject player)
    {
        _playerWeaponPickup = player.GetComponent<WeaponPickup>();

        if (!_playerWeaponPickup.IsPickUp)
        {
            _playerWeaponPickup.GunPickedUp();
            Debug.Log("El jugador recogió un arma.");
            Destroy(this.gameObject);
        }
    }
}
