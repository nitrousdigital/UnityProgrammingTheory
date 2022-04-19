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

    private SineCycle ySineCycle;

    public void Start()
    {
        ySineCycle = new SineCycle(playerSinWaveMagnitude, playerCycleSpeed, player);
        LaunchTorpedo();
    }

    void Update()
    {
        MovePlayer();   
    }

    private void MovePlayer()
    {
        ySineCycle.Update();
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
