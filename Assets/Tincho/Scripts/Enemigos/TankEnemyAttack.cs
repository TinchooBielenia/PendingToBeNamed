using TMPro;
using UnityEngine;

public class TankEnemyAttack : MonoBehaviour
{
    [SerializeField] private TankEnemy _tankEnemy;
    [SerializeField] private AudioSource _attackSFX;

    private void Start()
    {
        _tankEnemy = GetComponentInParent<TankEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //_tankEnemy.OnPlayerInAttackRange(true);
            Debug.Log("El jugador entro en la zona de ataque del enemigo.");
            _tankEnemy._playerInAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //_tankEnemy.OnPlayerInAttackRange(false);
            Debug.Log("El jugador salio de la zona de ataque del enemigo.");
            _tankEnemy._playerInAttackRange = false;
        }
    }
}
