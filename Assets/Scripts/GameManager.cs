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

    public void OnPlayerCrashedIntoEnemyShip(GameObject player, GameObject enemy)
    {
        Debug.Log("Player crashed into enemy");
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.Explode();

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.Explode();
    }

    public void OnEnemyHitByMissile(GameObject playerTorpedo, GameObject enemy)
    {
        Debug.Log("Player destroyed enemy");

        PlayerTorpedoController torpedoController = playerTorpedo.GetComponent<PlayerTorpedoController>();
        torpedoController.Explode();

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.Explode();
    }

}
