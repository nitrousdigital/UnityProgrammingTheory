using System;
using UnityEngine;

/// <summary>
///  Interface implemented by GameObjects that can explode
/// </summary>
public interface Explodable
{
    /// <summary>
    ///  Play the explosion animation and Destroy or deactivate the Explodable
    /// </summary>
    void Explode();
}
