using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealing player = other.GetComponent<PlayerHealing>();
            if (player != null)
            {
                player.TakeDamage(_damageAmount);
            }
        }
    }
}
