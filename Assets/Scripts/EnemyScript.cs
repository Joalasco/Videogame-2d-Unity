using System.Collections;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 2.0f;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
