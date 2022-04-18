using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Base class for game object controllers that can play
///  an explode animation and sound.
/// </summary>
public class ExplodableController : MonoBehaviour, Explodable
{
    /// <summary>
    ///  The explosion prefab to be instantiated when the enemy is destroyed.
    /// </summary>
    [SerializeField] private GameObject explosionPrefab;

    /// <summary>
    ///  The explosion sound to be played, or null
    /// </summary>
    [SerializeField] private AudioClip explosionSound;

    /// <summary>
    ///  The volume of the explosion sound
    /// </summary>
    [SerializeField] private float explosionSoundVolume = 1.0f;

    public void Explode()
    {
        // play the explosion effect
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = gameObject.transform.position;

        // play optional explosion sound
        if (explosionSound != null)
        {
            AudioManager.instance.Play(explosionSound, explosionSoundVolume);
        }

        // dispose of (or recycle) the explodable game object
        OnExploded();
    }

    /// <summary>
    ///  Remove this Explodable from the view.
    ///  By default this will call Destroy(gameObject).
    ///  If in a Pool, this method should be overridden
    ///  to call gameObject.SetActive(false) instead.
    /// </summary>
    protected virtual void OnExploded() {
        Destroy(gameObject);
    }
}
