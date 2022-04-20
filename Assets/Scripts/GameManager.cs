using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
///  The main controller for the in-game scene.
///  Manages the overall game state.
/// </summary>
public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        LEVEL_ANNOUNCE,
        PLAYING,
        GAME_OVER,
    }

    [SerializeField] private List<GameObject> ammoHud;

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject levelAnnounceCanvas;
    [SerializeField] private TextMeshProUGUI levelAnnounceText;

    [SerializeField] private int enemiesPerLevel = 10;

    // HUD fields
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private bool cheatMode = false;

    private EnemySpawnManager enemySpawnManager;

    private GameState state;
    private int level;
    private int score;
    private int enemiesRemaining;

    // ENCAPSULATION
    /// <summary>
    ///  Returns the current level being played.
    /// </summary>
    public int GetLevel()
    {
        return level;
    }

    private void Start()
    {
        enemySpawnManager = FindObjectOfType<EnemySpawnManager>();
        score = 0;
        StartLevel(1);
    }

    // ABSTRACTION
    /// <summary>
    ///  Update the ammo HUD UI
    /// </summary>
    public void UpdateAmmoHUD(int ammo)
    {
        for (int i = 0; i < ammoHud.Count; i++)
        {
            if (ammoHud[i] != null)
            {
                ammoHud[i].SetActive(i + 1 <= ammo);
            }
        }
    }

    // ABSTRACTION
    /// <summary>
    ///  Schedule the start of the nexy level
    /// </summary>
    private void ScheduleNextLevel()
    {
        StartLevel(level + 1);
    }

    // ABSTRACTION
    /// <summary>
    ///  Schedule the start of the specified level
    /// </summary>
    private void StartLevel(int level) {
        this.level = level;
        enemiesRemaining = enemiesPerLevel;
        levelAnnounceText.SetText("Level " + level);
        UpdateHUD();
        state = GameState.LEVEL_ANNOUNCE;
        ShowUiForState();
        Invoke("BeginLevel", 3);
    }

    // ABSTRACTION
    /// <summary>
    ///  Update the level and score HUD UI elements
    /// </summary>
    private void UpdateHUD()
    {
        levelText.SetText("Level: " + level);
        scoreText.SetText("Score: " + score);
    }

    // ABSTRACTION
    /// <summary>
    ///  Update the game state and UI to begin playing the level
    /// </summary>
    private void BeginLevel()
    {
        enemySpawnManager.OnLevelStarted(level);
        state = GameState.PLAYING;
        ShowUiForState();
    }

    // ENCAPSULATION
    /// <summary>
    ///  Returns true if gameplay is active and the player can be injured
    ///  and enemies can be destroyed
    /// </summary>
    public bool IsGamePlayActive()
    {
        return state == GameState.PLAYING || state == GameState.LEVEL_ANNOUNCE;
    }

    // ABSTRACTION
    /// <summary>
    ///  Schedule display of the game over text
    /// </summary>
    private void ScheduleGameOver()
    {
        if (state != GameState.GAME_OVER)
        {
            state = GameState.GAME_OVER;
            ShowUiForState();
            Invoke("ShowTitleScreen", 5);
        }
    }

    // ABSTRACTION
    /// <summary>
    ///  Load the title scene
    /// </summary>
    private void ShowTitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    // ABSTRACTION
    /// <summary>
    ///  Toggle visibility of UI components based upon the current GameState
    /// </summary>
    private void ShowUiForState()
    {
        gameOverCanvas.SetActive(state == GameState.GAME_OVER);
        levelAnnounceCanvas.SetActive(state == GameState.LEVEL_ANNOUNCE);
    }

    // ABSTRACTION
    /// <summary>
    ///  Update the game state when the player is hit by an enemy torpedo
    /// </summary>
    public void OnPlayerHitByEnemyMissile(GameObject player, GameObject enemyMissile)
    {
        if (!IsGamePlayActive() || cheatMode)
        {
            return;
        }

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.Explode();

        ExplodableController missile = enemyMissile.GetComponent<ExplodableController>();
        missile.Explode();

        ScheduleGameOver();
    }

    // ABSTRACTION
    /// <summary>
    ///  Update the game state when the player crashes into an enemy ship
    /// </summary>
    public void OnPlayerCrashedIntoEnemyShip(GameObject player, GameObject enemy)
    {
        if (!IsGamePlayActive() || cheatMode)
        {
            return;
        }

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.Explode();

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.Explode();

        ScheduleGameOver();
    }

    // ABSTRACTION
    /// <summary>
    ///  Update the game state when an enemy is hit by a player torpedo
    /// </summary>
    public void OnEnemyHitByMissile(GameObject playerTorpedo, GameObject enemy)
    {
        if (!IsGamePlayActive())
        {
            return;
        }

        PlayerTorpedoController torpedoController = playerTorpedo.GetComponent<PlayerTorpedoController>();
        torpedoController.Explode();

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.Explode();

        AwardScore(enemyController.GetScoreAward());

        enemiesRemaining--;
        Debug.Log("Enemies remaining = " + enemiesRemaining);
        if (enemiesRemaining <= 0)
        {
            ScheduleNextLevel();
        }
    }

    // ABSTRACTION
    /// <summary>
    ///  Update the game state when an enemy missile is hit by a player torpedo
    /// </summary>
    public void OnEnemyMissileDestroyed(GameObject playerTorpedo, GameObject enemyTorpedo)
    {
        if (!IsGamePlayActive())
        {
            return;
        }

        PlayerTorpedoController torpedoController = playerTorpedo.GetComponent<PlayerTorpedoController>();
        torpedoController.Explode();

        EnemyTorpedoController enemyTorpedoController = enemyTorpedo.GetComponent<EnemyTorpedoController>();
        enemyTorpedoController.Explode();

        AwardScore(enemyTorpedoController.GetScoreAward());
    }

    // ABSTRACTION
    /// <summary>
    ///  Update the player's score and refresh the HUD
    /// </summary>
    private void AwardScore(int score)
    {
        if (!IsGamePlayActive())
        {
            return;
        }

        this.score += score;
        UpdateHUD();
    }

}
