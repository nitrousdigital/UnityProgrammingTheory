using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    /// <summary>
    ///  The size of the sin wave (distance in pixels from the vertical origin)
    /// </summary>
    [SerializeField] private float playerSinWaveMagnitude = 0.3f;

    /// <summary>
    ///  The speed at which the ship will move along the sine wave
    /// </summary>
    [SerializeField] private float playerCycleSpeed = 0.5f;


    [SerializeField] private GameObject enemyTorpedoPrefab;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;

    /// <summary>
    ///  The sine wave degree in radians
    ///  Repeatedly Cycles between 0 and 2PI at a rate of cycleSpeed
    /// </summary>
    private float playerSinRad;
    /// <summary>
    ///  The current vertical offset from the origin where the enemy was instantiated
    /// </summary>
    private float playerVerticalOffset;

    /// <summary>
    ///  The initial vertical position about which we will be forming a sin wave
    /// </summary>
    private float playerOriginY;

    private static float PI2 = Mathf.PI * 2f;

    public void Start()
    {
        playerOriginY = player.transform.position.y;
        playerVerticalOffset = 0f;
        playerSinRad = 0f;

        LaunchTorpedo();
    }

    void Update()
    {
        MovePlayer();   
    }

    private void MovePlayer()
    {
        playerSinRad += playerCycleSpeed * Time.deltaTime;
        playerSinRad %= PI2;
        playerVerticalOffset = playerSinWaveMagnitude * Mathf.Sin(playerSinRad);
        float y = playerOriginY + playerVerticalOffset;
        Vector3 position = new Vector3(
            player.transform.position.x,
            y,
            player.transform.position.z);
        player.transform.position = position;
    }

    public void LaunchTorpedo()
    {
        GameObject torpedo = Instantiate(enemyTorpedoPrefab);
        torpedo.transform.position = enemy.transform.position;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
