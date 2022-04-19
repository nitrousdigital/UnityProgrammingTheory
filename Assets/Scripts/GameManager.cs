using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    private EnemySpawnManager enemySpawnManager;

    private GameState state;
    private int level;
    private int score;
    private int enemiesRemaining;

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

    private void ScheduleNextLevel()
    {
        StartLevel(level + 1);
    }

    private void StartLevel(int level) {
        this.level = level;
        enemiesRemaining = enemiesPerLevel;
        levelAnnounceText.SetText("Level " + level);
        UpdateHUD();
        state = GameState.LEVEL_ANNOUNCE;
        ShowUiForState();
        Invoke("BeginLevel", 3);
    }

    private void UpdateHUD()
    {
        levelText.SetText("Level: " + level);
        scoreText.SetText("Score: " + score);
    }

    private void BeginLevel()
    {
        enemySpawnManager.OnLevelStarted(level);
        state = GameState.PLAYING;
        ShowUiForState();
    }


    /// <summary>
    ///  Returns true if gameplay is active and the player can be injured
    ///  and enemies can be destroyed
    /// </summary>
    public bool IsGamePlayActive()
    {
        return state == GameState.PLAYING || state == GameState.LEVEL_ANNOUNCE;
    }

    private void ScheduleGameOver()
    {
        if (state != GameState.GAME_OVER)
        {
            state = GameState.GAME_OVER;
            ShowUiForState();
            Invoke("ShowTitleScreen", 5);
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
        gameOverCanvas.SetActive(state == GameState.GAME_OVER);
        levelAnnounceCanvas.SetActive(state == GameState.LEVEL_ANNOUNCE);
    }

    public void OnPlayerHitByEnemyMissile(GameObject player, GameObject enemyMissile)
    {
        if (!IsGamePlayActive())
        {
            return;
        }

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.Explode();

        ExplodableController missile = enemyMissile.GetComponent<ExplodableController>();
        missile.Explode();

        ScheduleGameOver();
    }

    public void OnPlayerCrashedIntoEnemyShip(GameObject player, GameObject enemy)
    {
        if (!IsGamePlayActive())
        {
            return;
        }

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.Explode();

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.Explode();

        ScheduleGameOver();
    }

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
