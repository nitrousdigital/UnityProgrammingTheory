using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ExplodableController
{
    /// <summary>
    ///  True if this enemy launches torpedos
    /// </summary>
    [SerializeField] private bool torpedosEquipped = false;

    /// <summary>
    ///  Speed of torpedos launched by this enemy
    /// </summary>
    [SerializeField] private float topedoSpeed = 1f;

    /// <summary>
    ///  The initial delay (seconds) before launching the first torpedo.
    /// </summary>
    [SerializeField] private float initialShootingDelay = 1f;

    /// <summary>
    ///  Minimum interval (seconds) between launching torpedos.
    /// </summary>
    [SerializeField] private float minShootingInterval = 1f;

    /// <summary>
    ///  Maximum interval (seconds) between launching torpedos.
    /// </summary>
    [SerializeField] private float maxShootingInterval = 2f;

    /// <summary>
    ///  Horizontal offset from enemy position where torpedo is to be instantiated
    /// </summary>
    [SerializeField] private float hTorpedoOffset = 0f;

    /// <summary>
    ///  Vertical offset from enemy position where torpedo is to be instantiated
    /// </summary>
    [SerializeField] private float vTorpedoOffset = 0f;

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

    /// <summary>
    ///  The TorpedoManager to be used to launch torpedos
    ///  from this enemy.
    /// </summary>
    private TorpedoManager torpedoManager;

    private GameManager gameManager;

    // Start is called before the first frame update
    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (torpedosEquipped)
        {
            torpedoManager = GameObject.Find("EnemyTorpedoManager").GetComponent<TorpedoManager>();
            ScheduleTorpedoLaunch(initialShootingDelay);
        }
    }

    /// <summary>
    ///  Schedule launching of a torpedo.
    /// </summary>
    private void ScheduleTorpedoLaunch(float delay)
    {
        Invoke("LaunchTorpedo", delay);
    }

    /// <summary>
    ///  Launch a torpedo and schedule the next launch
    /// </summary>
    private void LaunchTorpedo()
    {
        if (gameObject != null && gameObject.activeSelf)
        {
            torpedoManager.FireTorpedo(
                gameObject.transform.position.x + hTorpedoOffset,
                gameObject.transform.position.y + vTorpedoOffset,
                topedoSpeed);
            ScheduleNextTorpedoLaunch();
        }
    }

    /// <summary>
    ///  Schedule the next torpedo launch
    /// </summary>
    private void ScheduleNextTorpedoLaunch()
    {
        ScheduleTorpedoLaunch(Random.Range(minShootingInterval, maxShootingInterval));
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
