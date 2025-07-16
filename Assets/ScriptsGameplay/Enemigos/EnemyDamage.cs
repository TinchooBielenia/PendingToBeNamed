using UnityEngine;
//TP2 - Juliana Dimeglio
public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(_damageAmount);
            }
        }
    }
}
