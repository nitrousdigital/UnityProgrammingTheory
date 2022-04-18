using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AbstractExplodable
{
    /// <summary>
    ///  Minimum horizontal position of enemy before being considered
    ///  out of bounds and eligible for disposal
    /// </summary>
    [SerializeField] private float minX = -2f;


    /// <summary>
    ///  Horizontal speed
    /// </summary>
    [SerializeField] private float horizontalSpeed = 1.5f;

    /// <summary>
    ///  The maximum vertical speed
    /// </summary>
    [SerializeField] private float maxVerticalSpeed = 1.5f;

    /// <summary>
    ///  The current speed at which the enemy is moving vertically
    /// </summary>
    private float verticalSpeed;

    /// <summary>
    ///  The current rate at which the vertical speed is changing
    /// </summary>
    private float verticalSpeedChangeRate = 0.1f;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        verticalSpeed = Random.Range(-verticalSpeed, +verticalSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        MoveVertical();
        MoveHorizontal();
        CheckOutOfBounds();
    }

    /// <summary>
    ///  Move the enemy forward
    /// </summary>
    void MoveHorizontal()
    {
        transform.Translate(Vector3.forward * horizontalSpeed * Time.deltaTime);
    }

    /// <summary>
    ///  Move the enemy up/down in a waveform motion
    /// </summary>
    void MoveVertical()
    {
        // vertical speed follows a waveform
        // from +maxVerticalSpeed to -maxVerticalSpeed
        verticalSpeed += verticalSpeedChangeRate;
        if (verticalSpeed > maxVerticalSpeed)
        {
            verticalSpeed = maxVerticalSpeed;
            verticalSpeedChangeRate = -verticalSpeedChangeRate;
        }
        else if (verticalSpeed < -maxVerticalSpeed)
        {
            verticalSpeed = -maxVerticalSpeed;
            verticalSpeedChangeRate = -verticalSpeedChangeRate;
        }

        transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.OnPlayerCrashedIntoEnemyShip(
                collision.gameObject,
                gameObject);
        }
        else if (collision.CompareTag("PlayerMissile"))
        {
            gameManager.OnEnemyHitByMissile(
                collision.gameObject,
                gameObject);
        }
    }

    /// <summary>
    ///  Check whether this enemy has flown outside of the visible bounds of the screen
    ///  and if so, it should be disposed.
    /// </summary>
    void CheckOutOfBounds()
    {
        if (transform.position.x < minX)
        {
            Destroy(gameObject);
        }
    }

}
