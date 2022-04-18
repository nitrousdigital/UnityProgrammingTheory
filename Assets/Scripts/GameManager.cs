using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        TITLE_SCREEN,
        PLAYING,
        GAME_OVER,
    }

    private GameState state;

    private void Start()
    {
        state = GameState.PLAYING;
        ShowUiForState();
    }

    public bool IsPlaying()
    {
        return state == GameState.PLAYING;
    }



    private void ScheduleGameOver()
    {
        if (state != GameState.GAME_OVER)
        {
            state = GameState.GAME_OVER;
            ShowUiForState();
            Invoke("ShowTitleScreen", 3);
        }
    }

    /// <summary>
    ///  Load the title scene
    /// </summary>
    private void ShowTitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    ///  Toggle visibility of UI components based upon the current GameState
    /// </summary>
    private void ShowUiForState()
    {
        GameObject gameOverUi = GameObject.FindGameObjectWithTag("GameOver");
        if (gameOverUi != null)
        {
            gameOverUi.SetActive(state == GameState.GAME_OVER);
        }
    }

    public void OnPlayerCrashedIntoEnemyShip(GameObject player, GameObject enemy)
    {
        Debug.Log("Player crashed into enemy");
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.Explode();

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.Explode();

        ScheduleGameOver();
    }

    public void OnEnemyHitByMissile(GameObject playerTorpedo, GameObject enemy)
    {
        Debug.Log("Player destroyed enemy");

        PlayerTorpedoController torpedoController = playerTorpedo.GetComponent<PlayerTorpedoController>();
        torpedoController.Explode();

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.Explode();
    }

}
