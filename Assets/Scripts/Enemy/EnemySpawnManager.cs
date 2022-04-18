using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    /// <summary>
    ///  Prefab enemies to spawn
    /// </summary>
    [SerializeField] private List<GameObject> enemyPrefabs;

    /// <summary>
    ///  Minimum spawn vertical position
    /// </summary>
    [SerializeField] private float minSpawnY = 0.3f;

    /// <summary>
    ///  Maximum spawn vertical position
    /// </summary>
    [SerializeField] private float maxSpawnY = 1.3f;

    /// <summary>
    ///  Horizontal spawn position
    /// </summary>
    [SerializeField] private float spawnX = 1.5f;

    /// <summary>
    ///  Minimum delay between spawning of enemies in seconds
    /// </summary>
    private float minSpawnDelay = 5f;

    /// <summary>
    ///  Maximum delay between spawning of enemies in seconds
    /// </summary>
    private float maxSpawnDelay = 2f;


    // Start is called before the first frame update
    void Start()
    {
        SpawnNow();
    }

    /// <summary>
    ///  Coroutine to schedule the next spawn
    /// </summary>
    private IEnumerator ScheduleSpawn()
    {
        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(delay);
        SpawnNow();
    }

    /// <summary>
    ///  Schedule the next spawn
    /// </summary>
    private void ScheduleNextSpawn()
    {
        StartCoroutine(ScheduleSpawn());
    }

    /// <summary>
    ///  Immediately spawn an enemy and schedule the next spawn
    /// </summary>
    private void SpawnNow()
    {
        // spawn a random enemy
        int enemyIdx = Random.Range(0, enemyPrefabs.Count);
        GameObject enemy = Instantiate(enemyPrefabs[enemyIdx]);

        // randomize vertical position
        float y = Random.Range(minSpawnY, maxSpawnY);
        enemy.transform.position = new Vector3(spawnX, y, enemy.transform.position.z);

        // schedule the next spawn
        ScheduleNextSpawn();
    }

}
