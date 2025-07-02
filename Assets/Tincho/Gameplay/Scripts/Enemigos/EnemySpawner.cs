using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

//TP2 - Juliana Dimeglio
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private float _spawnDelay = 0.3f;
    [SerializeField] private float _displayDuration;
    [SerializeField] private AudioSource _zombieDinerSFX;

    public void StartSpawning(int zombieAmount)
    {
        HordeWarningUI.Instance?.ShowWarning();
        StartCoroutine(SpawnHorde(zombieAmount));
    }

    private IEnumerator SpawnHorde(int zombieQuantity)
    {

        for (int i = 0; i < zombieQuantity; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            Vector3 spawnPos = transform.position + offset + Vector3.up * 1f;
            Instantiate(_zombiePrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(_spawnDelay);
        }
        if (_zombieDinerSFX != null)
        {
            _zombieDinerSFX.Stop(); 
        }
    }
}