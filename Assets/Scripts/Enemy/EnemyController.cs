using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : TorpedoLauncher
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
    ///  The score to be awarded for destroying this enemy
    /// </summary>
    [SerializeField] private int scoreAward = 5;

    /// <summary>
    ///  Minimum horizontal position of enemy before being considered
    ///  out of bounds and eligible for disposal
    /// </summary>
    [SerializeField] private float minX = -2f;

    /// <summary>
    ///  Minimum horizontal coordinate where enemies are invincible
    ///  to ensure enemies are not destroyed before they fly into the visible
    ///  region of the screen
    /// </summary>
    [SerializeField] private float invincibleX = 1.9f;

    /// <summary>
    ///  Horizontal speed
    /// </summary>
    [SerializeField] private float horizontalSpeed = 1.5f;

    /// <summary>
    ///  True to increase the speed of the enemy and its torpedos as the level increases
    /// </summary>
    [SerializeField] private bool increaseSpeedWithLevel = true;

    /// <summary>
    ///  True to decrease time between torpedos as the level increases
    /// </summary>
    [SerializeField] private bool decreaseTorpedoIntervalWithLevel = true;

    /// <summary>
    ///  Minimum interval (seconds) between launching torpedos.
    /// </summary>
    [SerializeField] private float minShootingInterval = 3f;

    /// <summary>
    ///  Maximum interval (seconds) between launching torpedos.
    /// </summary>
    [SerializeField] private float maxShootingInterval = 4f;

    /// <summary>
    ///  The initial delay (seconds) before launching the first torpedo.
    /// </summary>
    private float initialShootingDelay = 1f;

    private GameManager gameManager;

    // Start is called before the first frame update
    new public void Start()
    {
        base.Start();
        gameManager = FindObjectOfType<GameManager>();

        // increase speed of enemy and missile with level
        int level = gameManager.GetLevel();
        if (level > 1)
        {
            if (decreaseTorpedoIntervalWithLevel)
            {
                minShootingInterval = Mathf.Max(1f, 3f - level);
                maxShootingInterval = Mathf.Max(minShootingInterval, 5f - level);
            }

            if (increaseSpeedWithLevel)
            {
                float increase = 1f + (0.1f * level);
                horizontalSpeed *= increase;
                topedoSpeed *= increase;
            }
        }

        if (torpedosEquipped)
        {
            SetTorpedoManager(GameObject.Find("EnemyTorpedoManager").GetComponent<TorpedoManager>());
            ScheduleTorpedoLaunch(initialShootingDelay);
        }
    }

    /// <summary>
    ///  Schedule launching of a torpedo.
    /// </summary>
    private void ScheduleTorpedoLaunch(float delay)
    {
        Invoke("LaunchTorpedoIfActive", delay);
    }

    /// <summary>
    ///  Launch a torpedo and schedule the next launch
    /// </summary>
    private void LaunchTorpedoIfActive()
    {
        if (gameObject != null && gameObject.activeSelf)
        {
            LaunchTorpedo(topedoSpeed);
                //gameObject.transform.position.x,
                //gameObject.transform.position.y,
                //topedoSpeed);
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
        // ignore off-screen collisions.
        if (gameObject.transform.position.x >= invincibleX)
        {
            return;
        }

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
