using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool isPaused = false;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void Update()
    {
        // 只在非輸入遮斷狀態下偵測（可擴充判斷例如在 console / chat open 時不處理）
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused) Resume();
        else Pause();
    }

    public void Pause()
    {
        if (isPaused) return;
        isPaused = true;
        Time.timeScale = 0f;

        var level = LevelGameManager.Instance;
        if (level != null)
        {
            level.PauseGame();
            return;
        }

        // fallback：若不是在關卡場景或 LevelGameManager 不存在，直接用 UI 打開暫停
        UIRegistry.Instance?.GetCurrent()?.OpenPause();
    }

    public void Resume()
    {
        if (!isPaused) return;
        isPaused = false;
        Time.timeScale = 1f;

        var level = LevelGameManager.Instance;
        if (level != null)
        {
            level.ResumeGame();
            return;
        }

        var pause = FindObjectOfType<PausePanel>();
        if (pause != null) pause.Close();
    }


    // 回主選單：清掉所有 Level 場景並載入 MainMenu (single load)
    public void GoToMainMenu()
    {
        StartCoroutine(GoToMainMenuRoutine());
    }

    IEnumerator LoadLevelRoutine(string levelSceneName)
    {
        // Optional: show loading UI
        // 卸載非必需場景或載入時顯示 loading
        // Load scene single (會卸載其他場景，Bootstrap 被放在 persistent managers，確保不被 unload)
        AsyncOperation op = SceneManager.LoadSceneAsync(levelSceneName, LoadSceneMode.Single);
        while (!op.isDone) yield return null;
        Debug.Log("[GlobalGameManager] Loaded level: " + levelSceneName);
    }

    IEnumerator GoToMainMenuRoutine()
    {
        // restore time scale
        Time.timeScale = 1f;

        // 如果你有要先儲存進度可以在這處理

        AsyncOperation op = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        while (!op.isDone) yield return null;

        Debug.Log("[GlobalGameManager] Back to Main Menu");
    }
}
