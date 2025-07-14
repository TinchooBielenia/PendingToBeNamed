using UnityEngine;

//TP2 - Martin Bielenia

public class PickUpLoot : MonoBehaviour
{
    [SerializeField] private AudioSource _ammoBoxSFX;
    [SerializeField] private AudioSource _medkitSFX;
    [SerializeField] private AudioSource _pickUpGunSFX;
    [SerializeField] private float _medkitHeal;
    private PlayerShootStats _playerAmmoStats;
    private PlayerHealth _playerHealthStats;
    private WeaponPickup _weaponPickUp;

    private void Start()
    {
        _playerAmmoStats = GetComponent<PlayerShootStats>();
        _playerHealthStats = GetComponent<PlayerHealth>();
        _weaponPickUp = GetComponent<WeaponPickup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        LootIdentifier loot = other.GetComponent<LootIdentifier>();
        if (loot == null) return;

        switch (loot.lootType)
        {
            case LootType.Ammo:
                if (_playerAmmoStats.MagazineSize != _playerAmmoStats.FullMagazineSize)
                {
                    _ammoBoxSFX.Play();
                    Debug.Log("El jugador recogió munición.");
                    _playerAmmoStats.TryReload();
                    Destroy(other.gameObject);
                }
                break;

            //case LootType.Medkit:
            //    if (_playerHealthStats.GetPlayerLife() < 100)
            //    {
            //        _medkitSFX.Play();
            //        Debug.Log("El jugador recogió un botiquín.");
            //        _playerHealthStats.HealPlayer(_medkitHeal);
            //        Destroy(other.gameObject);
            //    }
            //    break;

            case LootType.Gun:
                if (!_weaponPickUp.IsPickUp)
                {
                    _weaponPickUp.GunPickedUp();
                    Debug.Log("El jugador recogió un arma.");
                    _pickUpGunSFX.Play();
                    Destroy(other.gameObject);
                }
                break;
        }
    }
}
