using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AbstractExplodable
{
    /// <summary>
    ///  The number of player torpedos that can exist concurrently.
    /// </summary>
    [SerializeField] private int ammo = 3;

    /// <summary>
    ///  The torpedo prefab to be launched by the player
    /// </summary>
    [SerializeField] private GameObject torpedoPrefab;

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

    /// <summary>
    ///  Pool of reusable torpedos
    /// </summary>
    private List<GameObject> torpedos;

    // Start is called before the first frame update
    void Start()
    {
        InitTorpedoPool();
    }

    /// <summary>
    ///  Initialize the pool of reusable torpedos
    /// </summary>
    private void InitTorpedoPool()
    {
        torpedos = new List<GameObject>();
        for (int i = 0; i < ammo; i++)
        {
            GameObject torpedo = Instantiate(torpedoPrefab);
            torpedo.SetActive(false);
            torpedos.Add(torpedo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveVertical();
        MoveHorizontal();
        ClampVerticalPosition();
        ClampHorizontalPosition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        GameObject torpedo = FindTorpedo();
        if (torpedo == null)
        {
            return;
        }

        torpedo.transform.position = new Vector3(transform.position.x + missileOffsetX, transform.position.y, torpedo.transform.position.z);
        torpedo.SetActive(true);
    }

    /// <summary>
    ///  Find an available inactive torpedo in the pool.
    ///  Returns null if no torpedos are currently available.
    /// </summary>
    private GameObject FindTorpedo()
    {
        for (int i = 0; i < torpedos.Count; i++)
        {
            if (!torpedos[i].activeSelf)
            {
                return torpedos[i];
            }
        }
        return null;
    }

    void MoveVertical()
    {
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * vertical * Time.deltaTime * speed);
    }

    void MoveHorizontal()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * -horizontal * Time.deltaTime * speed);
    }

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
