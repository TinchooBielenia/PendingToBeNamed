using UnityEngine;

//TP2 - Martin Bielenia
public class MedkitLoot : BaseLoot
{
    private PlayerHealth _playerHealth;
    [SerializeField] private float _medkitHeal;

    protected override void OnLootTrigger(GameObject player)
    {
        _playerHealth = player.GetComponent<PlayerHealth>();
        if (_playerHealth.PlayerLife() < 100)
        {
            Debug.Log("El jugador recogió un botiquín.");
            _playerHealth.HealPlayer(_medkitHeal);
            Destroy(this.gameObject);
        }
    }
}
