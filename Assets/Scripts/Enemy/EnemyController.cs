using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ExplodableController
{
    /// <summary>
    ///  The score to be awarded for destroying this enemy
    /// </summary>
    [SerializeField] private int scoreAward = 5;

    /// <summary>
    ///  Minimum horizontal position of enemy before being considered
    ///  out of bounds and eligible for disposal
    /// </summary>
    [SerializeField] private float minX = -2f;

    /// <summary>
    ///  Horizontal speed
    /// </summary>
    [SerializeField] private float horizontalSpeed = 1.5f;

    private GameManager gameManager;

    // Start is called before the first frame update
    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    public void Update()
    {
        MoveVertical();
        MoveHorizontal();
        CheckOutOfBounds();
    }

    /// <summary>
    ///  The score to be awarded to the player for destroying this enemy
    /// </summary>
    public int GetScoreAward()
    {
        return scoreAward;
    }

    /// <summary>
    ///  Move the enemy forward
    /// </summary>
    protected virtual void MoveHorizontal()
    {
        transform.Translate(Vector3.forward * horizontalSpeed * Time.deltaTime);
    }

    /// <summary>
    ///  Hook for sub-classes to move the enemy along the vertical axis
    /// </summary>
    protected virtual void MoveVertical()
    {
    }

    /// <summary>
    ///  Handle collisions with the player or player torpedos
    /// </summary>
    protected void OnTriggerEnter(Collider collision)
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
    protected void CheckOutOfBounds()
    {
        if (transform.position.x < minX)
        {
            Destroy(gameObject);
        }
    }

}
