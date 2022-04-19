using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Controls torpedos that are launched by enemies.
///  Detects collisions with the player and player torpedos.
/// </summary>
public class EnemyTorpedoController : TorpedoController
{
    // score awarded for destroying this torpedo
    [SerializeField] private int scoreAward = 0;


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
    }

}
