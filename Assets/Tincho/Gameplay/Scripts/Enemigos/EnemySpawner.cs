using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private float _spawnDelay = 0.3f;
    //[SerializeField] private Camera _spawnerCamera;
    [SerializeField] private float _displayDuration;
    [SerializeField] private AudioSource _zombieDinerSFX;


    //private void Start()
    //{
    //    _spawnerCamera.enabled = false;
    //}

    public void StartSpawning(int zombieAmount)
    {
        HordeWarningUI.Instance?.ShowWarning();
        StartCoroutine(SpawnHorde(zombieAmount));
    }


    private IEnumerator SpawnHorde(int zombieQuantity)
    {
        //ShowSpawnerCamera();

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

    //public void ShowSpawnerCamera()
    //{
    //    if (_spawnerCamera != null)
    //    {
    //        _spawnerCamera.enabled = true;
    //        StartCoroutine(HideAfterDelay());
    //    }
    //}

    //private IEnumerator HideAfterDelay()
    //{
    //    yield return new WaitForSeconds(_displayDuration);
    //    _spawnerCamera.enabled = false;
    //}
}