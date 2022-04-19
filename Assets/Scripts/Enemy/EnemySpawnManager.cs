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
    private float minSpawnDelay = 2f;

    /// <summary>
    ///  Maximum delay between spawning of enemies in seconds
    /// </summary>
    private float maxSpawnDelay = 5f;

    private bool isScheduled = false;

    public void OnLevelStarted(int level)
    {
        // decrease spawn delay with increasing level
        maxSpawnDelay = Mathf.Max(6f - level, minSpawnDelay);
        Debug.Log("Enemy Max Spawn delay is now " + maxSpawnDelay);

        // if not already scheduled, spawn immediately and begin scheduled cycle
        if (!isScheduled)
        {
            SpawnNow();
        }
    }

    /// <summary>
    ///  Coroutine to schedule the next spawn
    /// </summary>
    private IEnumerator ScheduleSpawn()
    {
        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(delay);
        isScheduled = false;
        SpawnNow();
    }

    /// <summary>
    ///  Schedule the next spawn
    /// </summary>
    private void ScheduleNextSpawn()
    {
        if (isScheduled)
        {
            return;
        }
        isScheduled = true;
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
