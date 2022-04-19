using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Responsible for moving player launched torpedos.
///  Inherits the base torpedo controller behavior and
///  updates the HUD with the current ammo level when a torpedo
///  is activated/deactivated.
/// </summary>
public class PlayerTorpedoController : TorpedoController
{

    private PlayerTorpedoManager torpedoManager;

    private void Awake()
    {
        torpedoManager = FindObjectOfType<PlayerTorpedoManager>();
    }


    /// <summary>
    ///  Activate or deactivate this torpedo and update the ammo HUD
    /// </summary>
    public override void SetActive(bool active)
    {
        base.SetActive(active);
        torpedoManager.UpdateAmmoHud();
    }

    /// <summary>
    ///  Handle collisions with the enemy ships or enemy torpedos
    /// </summary>
    protected void OnTriggerEnter(Collider collision)
    {
        // handled by EnemyController to enable
        // enemy ship to be invincible when out of bounds
        //if (collision.CompareTag("EnemyShip"))
        //{
        //    gameManager.OnEnemyHitByMissile(
        //        gameObject,
        //        collision.gameObject);
        //}

        if (collision.CompareTag("EnemyMissile"))
        {
            gameManager.OnEnemyMissileDestroyed(
                gameObject,
                collision.gameObject);
        }
    }

}
