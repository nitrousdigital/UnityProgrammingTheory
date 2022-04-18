using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWaveEnemyController : EnemyController
{
    /// <summary>
    ///  The size of the sin wave (distance in pixels from the vertical origin)
    /// </summary>
    [SerializeField] private float sinWaveMagnitude = 0.5f;

    /// <summary>
    ///  The size of the sin wave (distance in pixels from the vertical origin)
    /// </summary>
    [SerializeField] private float cycleSpeed = 5f;

    /// <summary>
    ///  The current vertical offset from the origin where the enemy was instantiated
    /// </summary>
    private float verticalOffset;

    /// <summary>
    ///  The initial vertical position about which we will be forming a sin wave
    /// </summary>
    private float originY;

    /// <summary>
    ///  The sine wave degree in radians
    ///  Repeatedly Cycles between 0 and 2PI at a rate of cycleSpeed
    /// </summary>
    private float sinRad;

    private static float PI2 = Mathf.PI * 2f;

    new public void Start()
    {
        base.Start();
        originY = gameObject.transform.position.y;
        verticalOffset = 0f;
        sinRad = 0f;
    }

    protected override void MoveVertical()
    {
        sinRad += cycleSpeed * Time.deltaTime;
        sinRad %= PI2;
        verticalOffset = sinWaveMagnitude * Mathf.Sin(sinRad);
        float y = originY + verticalOffset;
        Vector3 position = new Vector3(
            transform.position.x,
            y,
            transform.position.z);
        transform.position = position;
    }



}
