using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorpedoManager : MonoBehaviour
{
    /// <summary>
    ///  The torpedo prefab to be launched by the player
    /// </summary>
    [SerializeField] private GameObject torpedoPrefab;

    /// <summary>
    ///  The number of player torpedos that can exist concurrently.
    /// </summary>
    [SerializeField] private int ammo = 3;

    /// <summary>
    ///  Pool of reusable torpedos
    /// </summary>
    private List<GameObject> torpedos;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        InitTorpedoPool();
        UpdateAmmoHud();
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
            torpedos.Add(torpedo);
            torpedo.SetActive(false);
        }
    }

    /// <summary>
    ///  Activate a torpedo, update the HUD and return the activated torpedo.
    ///  Returns null if no inactive torpedos are found.
    /// </summary>
    public GameObject FireTorpedo(float x, float y)
    {
        GameObject torpedo = FindTorpedo();
        if (torpedo == null)
        {
            return null;
        }
        torpedo.transform.position = new Vector3(x, y, torpedo.transform.position.z);
        PlayerTorpedoController torpedoController = torpedo.GetComponent<PlayerTorpedoController>();
        torpedoController.SetActive(true);
        UpdateAmmoHud();
        return torpedo;
    }

    /// <summary>
    ///  Update the HUD with the current ammo count
    /// </summary>
    public void UpdateAmmoHud()
    {
        gameManager.UpdateAmmoHUD(GetAmmoCount());
    }

    /// <summary>
    ///  Returns the current number of available torpedos to be launched.
    /// </summary>
    public int GetAmmoCount()
    {
        int ammo = 0;
        for (int i = 0; i < torpedos.Count; i++)
        {
            if (torpedos[i] != null && !torpedos[i].activeSelf)
            {
                ammo++;
            }
        }
        return ammo;
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

}
