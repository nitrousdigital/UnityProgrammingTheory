using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Manages a pool of torpedos that can be launched.
/// </summary>
public class TorpedoManager : MonoBehaviour
{
    /// <summary>
    ///  The torpedo prefab to be launched by the player
    /// </summary>
    [SerializeField] private GameObject torpedoPrefab;

    /// <summary>
    ///  The number of torpedos that can be concurrently active.
    /// </summary>
    [SerializeField] private int ammo = 3;

    /// <summary>
    ///  Pool of reusable torpedos
    /// </summary>
    private List<GameObject> torpedos;

    // Start is called before the first frame update
    public void Start()
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
            torpedos.Add(torpedo);
            torpedo.SetActive(false);
        }
    }

    /// <summary>
    ///  Launch a torpedo, if one is available.
    ///  Returns the torpedo that is being launched.
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
        TorpedoController torpedoController = torpedo.GetComponent<TorpedoController>();
        torpedoController.SetActive(true);
        return torpedo;
    }

    /// <summary>
    ///  Launch a torpedo, if one is available and set its speed.
    ///  Returns the torpedo that is being launched.
    ///  Returns null if no inactive torpedos are found.
    /// </summary>
    public GameObject FireTorpedo(float x, float y, float speed)
    {
        GameObject torpedo = FireTorpedo(x, y);
        if (torpedo != null)
        {
            torpedo.GetComponent<TorpedoController>().SetSpeed(speed);
        }
        return torpedo;
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
