using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private int _zombiesQuantity = 5; 
    [SerializeField] private float _spawnDelay = 0.3f;

     private void Start()
     {
         StartCoroutine(SpawnHorde());
     }

    public void StartSpawning()
    {
        StartCoroutine(SpawnHorde());
    }

    private IEnumerator SpawnHorde()
    {
        for (int i = 0; i < _zombiesQuantity; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            Vector3 spawnPos = transform.position + offset + Vector3.up * 1f;
            Instantiate(_zombiePrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}