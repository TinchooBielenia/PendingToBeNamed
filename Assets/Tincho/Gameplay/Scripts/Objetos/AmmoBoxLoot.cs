using UnityEngine;

public class AmmoBoxLoot : BaseLoot
{
    private PlayerShootStats _playerStats;
    [SerializeField] private AudioSource _ammoBoxSFX;

    protected override void OnLootTrigger(GameObject player)
    {
        _playerStats = player.GetComponent<PlayerShootStats>();
        if (_playerStats.MagazineSize != _playerStats.FullMagazineSize)
        {
            _ammoBoxSFX.Play();
            Debug.Log("El jugador recogió munición.");
            _playerStats.TryReload();
            Destroy(this.gameObject);
        }
    }
}
