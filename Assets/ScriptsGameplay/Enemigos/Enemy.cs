using System.Collections.Generic;
using UnityEngine;

//TP2 - Martin Bielenia
//Consigna: Abstract class
public abstract class Enemy : MonoBehaviour, IDamageEnemy<DamageData>
{
    [SerializeField] protected int _enemyLife;
    [SerializeField] protected int _maxEnemyLife = 100;
    protected Transform _transform;
    private bool _isDead;
    [SerializeField] private AudioSource _getDamagedSFX;
    protected bool _hasDroppedLoot;
    [SerializeField] protected List<GameObject> _lootList;
    [SerializeField] protected int _trapDamageMultiplier;
    [SerializeField] protected Rigidbody _rb;

    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
    }

    public virtual void TakeHit(DamageData data)
    {
        if (IsDead) return;
        _getDamagedSFX.Play();
        if (_enemyLife <= 0)
        {
            IsDead = true;
            _rb.isKinematic = false;
        }

        if (IsDead)
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders)
            {
                col.enabled = false;
            }

            LootOnDeath();

            Destroy(gameObject, 5f);
        }
    }

    protected virtual void TakeDamage(int damage)
    {
        if (IsDead) return;
        _enemyLife -= damage;
    }


    protected virtual void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody>();
    }

    protected virtual void LootOnDeath()
    {
        if (_hasDroppedLoot) return;

        _hasDroppedLoot = true;
    }
}
