using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] powerups;
    private bool _stopSpawning = false; 

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning is false)
        {
            Vector3 posToSpawn = new Vector3 (Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(3f, 6f));
        }
    }

    IEnumerator SpawnPowerupRoutine ()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning is false) 
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(powerups[Random.Range(0, 3)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 12f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
