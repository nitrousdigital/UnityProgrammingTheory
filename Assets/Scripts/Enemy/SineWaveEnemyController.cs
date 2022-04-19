using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    new public void Start()
    {
        base.Start();
        ySineCycle = new SineCycle(sinWaveMagnitude, cycleSpeed, gameObject);
    }

    protected override void MoveVertical()
    {
        ySineCycle.Update();
    }

}
