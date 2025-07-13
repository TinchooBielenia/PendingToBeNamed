using System.Collections.Generic;
using UnityEngine;

//TP2 - Martin Bielenia
public abstract class Enemy : MonoBehaviour, IDamageEnemy
{
    [SerializeField] protected int _enemyLife;
    [SerializeField] protected int _maxEnemyLife = 100;
    protected Transform _transform;
    private bool _isDead;
    [SerializeField] private AudioSource _getDamagedSFX;
    protected bool _hasDroppedLoot;
    [SerializeField] private List<GameObject> _lootList;

    public bool IsDead
    {
        get =>  _isDead; 
        set => _isDead = value; 
    }
    public virtual void TakeHit(int damage)
    {
        if(_isDead) return;
        _enemyLife -= damage;
        _getDamagedSFX.Play();
        if (_enemyLife < 0)
        {
            _isDead = true;
        }
    }


    protected virtual void Awake()
    {
        _transform = transform;
    }

    protected void LootOnDeath()
    {
        if (_hasDroppedLoot) return;

        if (_lootList == null || _lootList.Count == 0) return;

        foreach (GameObject loot in _lootList)
        {
            if (loot != null)
            {
                Instantiate(loot, _transform.position, Quaternion.identity);
            }
        }

        _hasDroppedLoot = true;
    }


}
