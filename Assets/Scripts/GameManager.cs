using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject supplicant;
    public int StarsCounter { get; set; } = 0;
    public bool isSliding = false;
    public bool CheckpointReached { get; set; } = false;
    public bool GracePeriodActive { get; private set; } = false;
    public bool CheckpointTriggeredOnce { get; set; } = false;

    public int CheckpointCounter { get; set; } = 0;

    public event System.Action OnImmunityRequested;
    public event System.Action OnRepairRequested;
    public event System.Action OnLegionRequested;

    // Player
    public GameObject playerPrefab;
    private GameObject currentPlayer;

    Vector2 screenBounds;
    public Transform player;

    private string[] scenesToDestroyOn = { "GameOverScene", "MenuScene" };

    protected override void Awake()
    {
        base.Awake();

        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (string sceneName in scenesToDestroyOn)
        {
            if (scene.name == sceneName)
            {
                Destroy(gameObject);
            }
        }
    }

    private new void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        player = GameObject.FindGameObjectWithTag("Player").transform;

        OnLegionRequested += Legions;
    }

    // Update is called once per frame
    void Update()
    {
        CheckpointCounterStorage.checkpointScore = CheckpointCounter;

        if (CheckpointReached)
        {
            return;
        }
        else
        {
            PauseMenuManager();
        }  
    }

    public void RequestRepair()
    {
        OnRepairRequested?.Invoke(); // Notify subscribers
    }
    public void RespawnPlayer(Vector3 position)
    {
        if (currentPlayer != null)
            Destroy(currentPlayer);

        currentPlayer = Instantiate(playerPrefab, position, Quaternion.identity);
    }

    public void RequestImmunity()
    {
        OnImmunityRequested?.Invoke(); // Notify subscribers
    }

    // Add this to find player whenever needed
    public Transform GetPlayer()
    {
        if (player == null || player.gameObject == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        return player;
    }

    public void Legions()
    {
        var currentPlayer = GetPlayer();
        if (currentPlayer == null) return;

        if (StarsCounter >= 5)
        {
            StarsCounter -= 5;
            for (int i = 0; i < Random.Range(2f, 10f); i++)
            {
                Instantiate(supplicant,
                    new Vector2(currentPlayer.position.x - 5,
                              currentPlayer.position.y + Random.Range(2, 10)),
                    Quaternion.identity);
            }
        }
    }


    public void RequestLegions()
    {
        OnLegionRequested?.Invoke(); // Notify subscribers
    }

    public void BeginGracePeriod(float duration = 5f)
    {
        StartCoroutine(GracePeriodCoroutine(duration));
    }

    private IEnumerator GracePeriodCoroutine(float duration)
    {
        GracePeriodActive = true;
        yield return new WaitForSeconds(duration);
        GracePeriodActive = false;
    }



    void PauseMenuManager()
    {
        // If the escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f) // Game is running, pause it
            {

                if (GameObject.FindGameObjectWithTag("PauseMenu") == null)
                {
                    Instantiate(Resources.Load("PauseMenu"));
                }

                Time.timeScale = 0f; // Pause the game
            }
            else if (Time.timeScale == 0f) // Game is paused, unpause it
            {

                // Destroy the pause menu or set it inactive
                Destroy(GameObject.FindGameObjectWithTag("PauseMenu"));

                Time.timeScale = 1f; // Resume the game
            }
        }
    }
    public void ResetCheckpointState()
    {
        CheckpointReached = false;
        // Any other state reset needed
    }
}
