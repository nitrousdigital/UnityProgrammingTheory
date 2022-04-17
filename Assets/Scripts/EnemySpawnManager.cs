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
    [SerializeField] private float maxSpawnY = 1.5f;

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
        ScheduleNextSpawn();
    }


    private IEnumerator ScheduleSpawn()
    {
        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(delay);
        SpawnNow();
        ScheduleNextSpawn();
    }

    private void ScheduleNextSpawn()
    {
        StartCoroutine(ScheduleSpawn());
    }

    private void SpawnNow()
    {
        int enemyIdx = Random.Range(0, enemyPrefabs.Count);
        GameObject enemy = Instantiate(enemyPrefabs[enemyIdx]);

        float y = Random.Range(minSpawnY, maxSpawnY);
        enemy.transform.position = new Vector3(spawnX, y, enemy.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
