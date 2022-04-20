using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE

/// <summary>
///  Extends the base controller for enemies
///  adding vertical movement along the path of a sine wave
/// </summary>
public class SineWaveEnemyController : EnemyController
{
    /// <summary>
    ///  The size of the sin wave (distance in pixels from the vertical origin)
    /// </summary>
    [SerializeField] private float sinWaveMagnitude = 0.5f;

    /// <summary>
    ///  The speed at which the ship will move along the sine wave
    /// </summary>
    [SerializeField] private float cycleSpeed = 5f;

    private SineCycle ySineCycle;

    // POLYMORPHISM
    new public void Start()
    {
        base.Start();
        ySineCycle = new SineCycle(sinWaveMagnitude, cycleSpeed, gameObject);
    }

    // POLYMORPHISM
    /// <summary>
    ///  Override the default vertical movement by moving the enemy along a sine wave
    /// </summary>
    protected override void MoveVertical()
    {
        ySineCycle.Update();
    }

}
