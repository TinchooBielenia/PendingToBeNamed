using UnityEngine;

//TP2 - Martin Bielenia
public class AmmoBoxLoot : BaseLoot
{
    private PlayerShootStats _playerStats;

    protected override void OnLootTrigger(GameObject player)
    {
        _playerStats = player.GetComponent<PlayerShootStats>();
        if (_playerStats.MagazineSize != _playerStats.FullMagazineSize)
        {
            Debug.Log("El jugador recogió munición.");
            _playerStats.TryReload();
            Destroy(this.gameObject);
        }
    }
}
