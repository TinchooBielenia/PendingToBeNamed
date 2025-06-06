using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealing player = other.GetComponent<PlayerHealing>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
        }
    }
}
