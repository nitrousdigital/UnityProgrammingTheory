using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Base class for game object controllers that can play
///  and explode animation.
/// </summary>
public abstract class AbstractExplodable : MonoBehaviour, Explodable
{
    /// <summary>
    ///  The explosion prefab to be instantiated when the enemy is destroyed.
    /// </summary>
    [SerializeField] private GameObject explosionPrefab;

    public void Explode()
    {
        // play the explosion effect
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = gameObject.transform.position;

        // dispose of (or recycle) the explodable game object
        OnDestroy();
    }

    /// <summary>
    ///  Remove this Explodable from the view.
    ///  By default this will call Destroy(gameObject).
    ///  If in a Pool, this method should be overridden
    ///  to call gameObject.SetActive(false) instead.
    /// </summary>
    protected virtual void OnDestroy() {
        Destroy(gameObject);
    }
}
