using System;
using UnityEngine;

public interface Explodable
{
    /// <summary>
    ///  Play the explosion animation and Destroy or deactivate the Explodable
    /// </summary>
    void Explode();
}
