using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE

/// <summary>
///  Base class for torpedo controllers on the title scene.
///  These controllers destroy and re-create torpedoes instead of recyclying torpedoes from a pool.
/// </summary>
public class TitleSceneTorpedoController : TorpedoController
{
    protected TitleSceneController titleScene;
    private void Awake()
    {
        titleScene = FindObjectOfType<TitleSceneController>();
    }

    // POLYMORPHISM
    /// <summary>
    ///  For the title screen we don't recycle torpedos, we just destroy and re-create
    /// </summary>
    public override void SetActive(bool active)
    {
        if (!active)
        {
            Destroy(gameObject);
            OnTorpedoDestroyed();
        }
    }

    /// <summary>
    ///  A hook for sub-classes to determine the behavior that should
    ///  occur after a torpedo has been destroyed.
    /// </summary>
    protected virtual void OnTorpedoDestroyed()
    {

    }
}
