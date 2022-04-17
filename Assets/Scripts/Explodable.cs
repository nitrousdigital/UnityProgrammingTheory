using System;
using UnityEngine;

public interface Explodable
{
    /// <summary>
    ///  Returns the explosion prefab to be instantiated when this Explodable is exploded.
    /// </summary>
    GameObject GetExplosionPrefab();
}
