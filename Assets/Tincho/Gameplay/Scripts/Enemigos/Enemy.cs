using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _enemyLife;
    [SerializeField] protected int _maxEnemyLife = 100;
    protected Transform _transform;
    protected bool _isDead;
    protected bool _hasDroppedLoot;
    protected List<GameObject> _lootList;


    protected virtual void Awake()
    {
        _transform = transform;
    }

    protected void GetDamage(int damage) => _enemyLife -= damage;


    protected virtual void Death()
    {
        if (_enemyLife <= 0)
        {
            _isDead = true;
            LootOnDeath(_lootList);

        }
    }

    protected void LootOnDeath(List<GameObject> lootList)
    {
        if (_hasDroppedLoot) return;

        if (lootList == null || lootList.Count == 0) return;

        foreach (GameObject loot in lootList)
        {
            if (loot != null)
            {
                Instantiate(loot, _transform.position, Quaternion.identity);
            }
        }

        _hasDroppedLoot = true;
    }


}
