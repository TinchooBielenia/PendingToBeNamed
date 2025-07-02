using UnityEngine;

public class PickUpLoot : MonoBehaviour
{
    [SerializeField] private AudioSource _ammoBoxSFX;
    //[SerializeField] private AudioSource _medkitSFX;
    //[SerializeField] private float _medkitHeal;
    [SerializeField] private PlayerShootStats _playerAmmoStats;
    [SerializeField] private PlayerHealth _playerHealthStats;

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
                    _playerAmmoStats.MagazineSize = _playerAmmoStats.FullMagazineSize;
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
        }
    }
}
