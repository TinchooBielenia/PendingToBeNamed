using UnityEngine;

public class TankEnemyAttack : MonoBehaviour
{
    [SerializeField] private TankEnemy _tankEnemy;

    private void Start()
    {
        _tankEnemy = GetComponentInParent<TankEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _tankEnemy.PlayerInAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _tankEnemy.PlayerInAttackRange = false;
        }
    }
}
