using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private void Awake()
    {
        // singleton GameManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerCrashedIntoEnemyShip(GameObject player, GameObject enemy)
    {
        Debug.Log("Player crashed into enemy");
        PlayerController playerController = player.GetComponent<PlayerController>();
        PlayExplosionEffect(playerController, player.transform.position);
        playerController.Explode();

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        PlayExplosionEffect(enemyController, enemy.transform.position);
        enemyController.Explode();
    }

    public void OnEnemyHitByMissile(GameObject playerTorpedo, GameObject enemy)
    {
        Debug.Log("Player destroyed enemy");

        PlayerTorpedoController torpedoController = playerTorpedo.GetComponent<PlayerTorpedoController>();
        PlayExplosionEffect(torpedoController, playerTorpedo.transform.position);
        torpedoController.Explode();

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        PlayExplosionEffect(enemyController, enemy.transform.position);
        enemyController.Explode();

    }

    public void PlayExplosionEffect(Explodable explodable, Vector3 location)
    {
        GameObject explosion = Instantiate(explodable.GetExplosionPrefab());
        explosion.transform.position = location;
    }

}
