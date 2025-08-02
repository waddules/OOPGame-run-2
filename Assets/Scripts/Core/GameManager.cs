using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseMenu;
    public GameObject gameOverUI;

    [Header("State Management")]
    public static bool isPaused;
    public static bool isGameOver;

    public static bool isShopOpen = false;


    private void Start()
    {
        // Initialize UI states
        pauseMenu.SetActive(false);
        gameOverUI.SetActive(false);

        // Initialize cursor state
        UpdateCursorState();
    }

    private void Update()
    {
        // Handle pause input only if game isn't over
        if (!isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Update cursor state continuously
        UpdateCursorState();
    }

    private void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        UpdateCursorState();
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        UpdateCursorState();
        Debug.Log("Game Resumed");
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 1f;
        isGameOver = true;
        UpdateCursorState();
        Debug.Log("Game Over");
    }

    private void UpdateCursorState()
    {
        if (isGameOver || isPaused || isShopOpen)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Game Restarted");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Returned to Main Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset all game states when a new scene loads
        isPaused = false;
        isGameOver = false;
        Time.timeScale = 1f;

        // Re-initialize UI references if needed
        if (pauseMenu == null)
            pauseMenu = GameObject.Find("PauseMenu");
        if (gameOverUI == null)
            gameOverUI = GameObject.Find("GameOverUI");

        // Ensure UI is hidden
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (gameOverUI != null) gameOverUI.SetActive(false);
    }
}