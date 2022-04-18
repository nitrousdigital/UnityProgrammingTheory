using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ExplodableController
{

    /// <summary>
    ///  The audio clip to be played when launching a torpedo
    /// </summary>
    [SerializeField] private AudioClip torpedoLaunchSound;

    /// <summary>
    ///  The audio level for the sound played when launching a torpedo
    /// </summary>
    [SerializeField] private float torpedoLaunchVolume = 0.5f;

    /// <summary>
    ///  Horizontal offset from the player where the torpedo should be displayed when launched
    /// </summary>
    [SerializeField] private float missileOffsetX = 0f;

    /// <summary>
    ///  Movement speed of the ship
    /// </summary>
    private float speed = 1f;

    /// <summary>
    ///  Minimum vertical position for player
    /// </summary>
    private float minY = 0.3f;

    /// <summary>
    ///  Maximum vertical position for player
    /// </summary>
    private float maxY = 1.5f;

    /// <summary>
    ///  Minimum horizontal position for player
    /// </summary>
    private float minX = -1.3f;

    /// <summary>
    ///  Maximum horizontal position for player
    /// </summary>
    private float maxX = 1.3f;

    private AudioSource audioPlayer;
    private PlayerTorpedoManager torpedoManager;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        torpedoManager = FindObjectOfType<PlayerTorpedoManager>();
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerMovement();
        ClampPosition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireWeapon();
        }
    }

    /// <summary>
    ///  Ensure the player does not move out of bounds
    /// </summary>
    private void ClampPosition()
    {
        ClampVerticalPosition();
        ClampHorizontalPosition();
    }

    /// <summary>
    ///  Move the player based on user input
    /// </summary>
    private void HandlePlayerMovement()
    {
        MoveVertical();
        MoveHorizontal();
    }

    /// <summary>
    ///  If an inactive torpedo is available then launch it.
    /// </summary>
    private void FireWeapon()
    {
        GameObject torpedo = torpedoManager.FireTorpedo(transform.position.x + missileOffsetX, transform.position.y);
        if (torpedo != null)
        {
            audioPlayer.PlayOneShot(torpedoLaunchSound, torpedoLaunchVolume);
        }
    }

    /// <summary>
    ///  Handle vertical movement
    /// </summary>
    void MoveVertical()
    {
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * vertical * Time.deltaTime * speed);
    }

    /// <summary>
    ///  Handle horizontal movement
    /// </summary>
    void MoveHorizontal()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * -horizontal * Time.deltaTime * speed);
    }

    /// <summary>
    ///  Clamp the player to the configured horizontal bounds
    /// </summary>
    void ClampHorizontalPosition()
    {
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
    }

    /// <summary>
    ///  Clamp the player to the configured vertical bounds
    /// </summary>
    void ClampVerticalPosition()
    {
        if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
        else if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
    }
}
