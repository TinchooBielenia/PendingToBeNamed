using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _enemyLife;
    [SerializeField] protected int _maxEnemyLife = 100;
    protected Transform _transform;
    protected bool _isDead;
    protected bool _hasDroppedLoot;
    [SerializeField] protected List<GameObject> _lootList;


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
            LootOnDeath();

        }
    }

    protected void LootOnDeath()
    {
        if (_hasDroppedLoot) return;

        if (_lootList == null || _lootList.Count == 0) return;

        int randomIndex = Random.Range(0, _lootList.Count);
        GameObject loot = _lootList[randomIndex];

        if (loot != null)
        {
            Vector3 spawnPosition = _transform.position + Vector3.up * 1f;
            GameObject spawnedLoot = Instantiate(loot, spawnPosition, Quaternion.identity);
            Destroy(spawnedLoot, 10f);
        }

        _hasDroppedLoot = true;
    }


}
