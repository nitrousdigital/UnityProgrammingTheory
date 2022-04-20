using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
///  Main controller for the title scene.
///  
///  Periodically launches player and enemy torpedos and animates movement for the player and enemy
/// </summary>
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

    [SerializeField] private GameObject playerTorpedoPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerTurret;

    private SineCycle playerSineCycle;
    private SineCycle enemySineCycle;

    public void Start()
    {
        playerSineCycle = new SineCycle(playerSinWaveMagnitude, playerCycleSpeed, player);
        enemySineCycle = new SineCycle(enemy, enemySinWaveMagnitude, enemyCycleSpeed);
        LaunchEnemyTorpedo();
        Invoke("LaunchPlayerTorpedo", 1f);
    }

    void Update()
    {
        MovePlayer();
        MoveEnemy();
    }

    // ABSTRACTION
    /// <summary>
    ///  Move the enemy ship in the title scene
    /// </summary>
    private void MoveEnemy()
    {
        enemySineCycle.Update();
    }

    // ABSTRACTION
    /// <summary>
    ///  Move the player ship in the title scene
    /// </summary>
    private void MovePlayer()
    {
        playerSineCycle.Update();
    }

    // ABSTRACTION
    /// <summary>
    ///  Launch a player torpedo in the title scene
    /// </summary>
    public void LaunchPlayerTorpedo()
    {
        GameObject torpedo = Instantiate(playerTorpedoPrefab);
        torpedo.transform.position = playerTurret.transform.position;
    }

    // ABSTRACTION
    /// <summary>
    ///  Launch an enemy torpedo in the title scene
    /// </summary>
    public void LaunchEnemyTorpedo()
    {
        GameObject torpedo = Instantiate(enemyTorpedoPrefab);
        torpedo.transform.position = enemy.transform.position;
    }

    // ABSTRACTION
    /// <summary>
    ///  Load the scene to play the game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // ABSTRACTION
    /// <summary>
    ///  Exit the application (or play mode when in dev mode)
    /// </summary>
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
