using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _enemyLife;
    [SerializeField] protected int _maxEnemyLife = 100;
    //[SerializeField] protected Animator _enemyWalkingAnim;
    protected Transform _transform;
    protected bool _isDead;

    protected virtual void Awake()
    {
        _transform = transform;
    }

    protected void GetDamage(int damage)
    {
        _enemyLife -= damage;
    }

    protected void Death()
    {
        if (_enemyLife <= 0)
        {
            Debug.Log("Enemy has died.");
            //gameObject.gameObject.SetActive(false);
            _isDead = true;

        }
    }

}
