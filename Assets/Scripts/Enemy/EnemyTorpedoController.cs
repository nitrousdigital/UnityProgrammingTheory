using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTorpedoController : TorpedoController
{
    // score awarded for destroying this torpedo
    [SerializeField] private int scoreAward = 0;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    ///  Returns the score to be awarded for destroying this torpedo
    /// </summary>
    public int GetScoreAward()
    {
        return scoreAward;
    }

    /// <summary>
    ///  Handle collisions with the player or player torpedos
    /// </summary>
    protected void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.OnPlayerHitByEnemyMissile(collision.gameObject, gameObject);
        }
        else if (collision.CompareTag("PlayerMissile"))
        {
            gameManager.OnEnemyMissileDestroyed(collision.gameObject, gameObject);
        }
    }

}
