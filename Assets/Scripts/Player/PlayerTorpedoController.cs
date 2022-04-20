using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE

/// <summary>
///  Responsible for moving torpedos that are launched by the player.
///  
///  Detects collisions with enemy torpedos and notifies the Torpedo Manager
///  when a torpedo is activated/deactivated to update the HUD with the current ammo level
/// </summary>
public class PlayerTorpedoController : TorpedoController
{

    private PlayerTorpedoManager torpedoManager;

    private void Awake()
    {
        torpedoManager = FindObjectOfType<PlayerTorpedoManager>();
    }


    // POLYMORPHISM
    /// <summary>
    ///  Override the default SetActive behavior in order to
    ///  update the ammo HUD
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
        if (collision.CompareTag("EnemyMissile"))
        {
            gameManager.OnEnemyMissileDestroyed(
                gameObject,
                collision.gameObject);
        }
    }

}
