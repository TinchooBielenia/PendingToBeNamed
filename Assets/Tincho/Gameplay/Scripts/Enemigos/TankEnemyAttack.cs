using UnityEngine;
//TP2 - Juliana Dimeglio
public class TankEnemyAttack : MonoBehaviour
{
    private TankEnemy _tankEnemy;
    [SerializeField] private GameObject _damageArea;

    private void Start()
    {
        _tankEnemy = GetComponentInParent<TankEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _tankEnemy.PlayerInAttackRange = true;
            EnableDamageCollider();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _tankEnemy.PlayerInAttackRange = false;
            DisableDamageCollider();
        }
    }

    private void EnableDamageCollider()
    {
        _damageArea.SetActive(true);
    }

    private void DisableDamageCollider()
    {
        _damageArea.SetActive(false);
    }
}