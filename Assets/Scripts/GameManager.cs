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

    // HUD fields
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;

    private PlayerController player;
    private GameState state;
    private int level;
    private int score;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        score = 0;
        StartLevel(1);
    }

    public void UpdateAmmoHUD(int ammo)
    {
        Debug.Log("Ammo HUD = " + ammo + " state=" + state);
        for (int i = 0; i < ammoHud.Count; i++)
        {
            if (ammoHud[i] != null)
            {
                ammoHud[i].SetActive(i + 1 <= ammo);
            }
        }
    }

    private void StartLevel(int level) {
        this.level = level;
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
        Debug.Log("Player hit by missile");
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.Explode();

        ExplodableController missile = enemyMissile.GetComponent<ExplodableController>();
        missile.Explode();

        ScheduleGameOver();
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

        AwardScore(enemyController.GetScoreAward());
    }

    public void OnEnemyMissileDestroyed(GameObject playerTorpedo, GameObject enemyTorpedo)
    {
        Debug.Log("Player destroyed an enemy torpedo");

        PlayerTorpedoController torpedoController = playerTorpedo.GetComponent<PlayerTorpedoController>();
        torpedoController.Explode();

        EnemyTorpedoController enemyTorpedoController = enemyTorpedo.GetComponent<EnemyTorpedoController>();
        enemyTorpedoController.Explode();

        AwardScore(enemyTorpedoController.GetScoreAward());
    }

    private void AwardScore(int score)
    {
        this.score += score;
        UpdateHUD();
    }

}
