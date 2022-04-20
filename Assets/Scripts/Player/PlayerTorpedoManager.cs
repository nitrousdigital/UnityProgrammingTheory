using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE

/// <summary>
///  Manages a pool of torpedos that can be launched by the player.
///  Updates the game HUD to show the number of available torpedos.
/// </summary>
public class PlayerTorpedoManager : TorpedoManager
{

    private GameManager gameManager;

    // POLYMORPHISM
    // Start is called before the first frame update
    new public void Start()
    {
        base.Start();
        gameManager = FindObjectOfType<GameManager>();
        UpdateAmmoHud();
    }

    // POLYMORPHISM
    /// <summary>
    ///  Overrides the base torpedo firing mechanism to update
    ///  the ammo HUD after a torpedo is launched.
    ///  
    ///  Returns null if no inactive torpedos are found.
    /// </summary>
    new public GameObject FireTorpedo(float x, float y)
    {
        GameObject torpedo = base.FireTorpedo(x, y);
        if (torpedo == null)
        {
            return null;
        }
        UpdateAmmoHud();
        return torpedo;
    }

    /// <summary>
    ///  Update the HUD with the current ammo count
    /// </summary>
    public void UpdateAmmoHud()
    {
        gameManager.UpdateAmmoHUD(GetAmmoCount());
    }



}
