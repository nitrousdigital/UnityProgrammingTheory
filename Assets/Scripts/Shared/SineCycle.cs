using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Moves a GameObject along a Sine wave.
///  Affects either the x-axis, y-axis or both.
/// </summary>

public class SineCycle
{
    private GameObject gameObject;

    /// <summary>
    /// the size of the sine wave for the x-axis movement
    /// </summary>
    private float xSineMagnitude;
    /// <summary>
    /// the speed to move the x-axis along the path of a sine wave.
    /// </summary>
    private float xCycleSpeed;
    /// <summary>
    /// original x position obtained during construction of this SineCycle
    /// </summary>
    private float xOrigin;
    /// <summary>
    /// current horizontal distance from xOrigin
    /// </summary>
    private float xOffset;
    /// <summary>
    ///  The sine wave degree in radians for the x-axis.
    ///  Repeatedly Cycles between 0 and 2PI at a rate of cycleSpeed
    /// </summary>
    private float xSinRad;

    /// <summary>
    /// the size of the sine wave for the y-axis movement
    /// </summary>
    private float ySineMagnitude;
    /// <summary>
    /// the speed to move the y-axis along the path of a sine wave.
    /// </summary>
    private float yCycleSpeed;
    /// <summary>
    /// original y position obtained during construction of this SineCycle
    /// </summary>
    private float yOrigin;
    /// <summary>
    /// current vertical distance from yOrigin
    /// </summary>
    private float yOffset;
    /// <summary>
    ///  The sine wave degree in radians for the y-axis.
    ///  Repeatedly Cycles between 0 and 2PI at a rate of cycleSpeed
    /// </summary>
    private float ySinRad;


    /// <summary>
    ///  True if this SineCycle is to affect the x-xcis
    /// </summary>
    private bool isMoveX;

    /// <summary>
    ///  True if this SineCycle is to affect the y-xcis
    /// </summary>
    private bool isMoveY;

    /// <summary>
    ///  Construct a SineCycle to move the x-axis of the specified GameObject
    ///  along the path of a sine wave.
    /// </summary>
    public SineCycle(GameObject gameObject,
                     float xSineMagnitude,
                     float xCycleSpeed) :
        this(gameObject,
             xSineMagnitude,
             xCycleSpeed,
             0f, 0f)
    {
        isMoveY = false;
    }

    /// <summary>
    ///  Construct a SineCycle to move the y-axis of the specified GameObject
    ///  along the path of a sine wave.
    /// </summary>
    public SineCycle(float ySineMagnitude,
                     float yCycleSpeed,
                     GameObject gameObject) :
        this(gameObject,
             0f, 0f,
             ySineMagnitude,
             yCycleSpeed)
    {
        isMoveX = false;
    }

    /// <summary>
    ///  Construct a SineCycle to move the x-axis and y-axis of the specified GameObject
    ///  along the path of a sine wave.
    /// </summary>
    public SineCycle(GameObject gameObject,
        float xSineMagnitude,
        float xCycleSpeed,
        float ySineMagnitude,
        float yCycleSpeed)
    {
        isMoveX = true;
        isMoveY = true;

        this.gameObject = gameObject;
        this.xSineMagnitude = xSineMagnitude;
        this.xCycleSpeed = xCycleSpeed;
        this.ySineMagnitude = ySineMagnitude;
        this.yCycleSpeed = yCycleSpeed;

        this.xOffset = 0f;
        this.yOffset = 0f;
        this.xOrigin = gameObject.transform.position.x;
        this.yOrigin = gameObject.transform.position.y;
    }

    public void Update()
    {
        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;
        if (isMoveX)
        {
            xSinRad += xCycleSpeed * Time.deltaTime;
            xSinRad %= MathUtil.PI2;
            xOffset = xSineMagnitude * Mathf.Sin(xSinRad);
            x = xOrigin + xOffset;
        }
        if (isMoveY)
        {
            ySinRad += yCycleSpeed * Time.deltaTime;
            ySinRad %= MathUtil.PI2;
            yOffset = ySineMagnitude * Mathf.Sin(ySinRad);
            y = yOrigin + yOffset;
        }

        Vector3 position = new Vector3(
            x,
            y,
            gameObject.transform.position.z);
        gameObject.transform.position = position;
    }
}
