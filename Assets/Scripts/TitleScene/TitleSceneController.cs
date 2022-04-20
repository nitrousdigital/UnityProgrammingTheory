using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

    /// <summary>
    ///  The size of the sin wave (distance in pixels from the horizontal origin)
    /// </summary>
    [SerializeField] private float enemySinWaveMagnitude = 0.3f;

    /// <summary>
    ///  The speed at which the ship will move along the sine wave
    /// </summary>
    [SerializeField] private float enemyCycleSpeed = 0.5f;

    [SerializeField] private GameObject enemyTorpedoPrefab;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;

    private SineCycle playerSineCycle;
    private SineCycle enemySineCycle;

    public void Start()
    {
        playerSineCycle = new SineCycle(playerSinWaveMagnitude, playerCycleSpeed, player);
        enemySineCycle = new SineCycle(enemy, enemySinWaveMagnitude, enemyCycleSpeed);
        LaunchTorpedo();
    }

    void Update()
    {
        MovePlayer();
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        enemySineCycle.Update();
    }

    private void MovePlayer()
    {
        playerSineCycle.Update();
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

    /**
     * Exit the application (or play mode when in dev mode)
     */
    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            // production/runtime code to quit Unity player
            Application.Quit(); 
        #endif
    }
}
