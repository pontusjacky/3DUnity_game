using UnityEngine;

public class LevelGameManager : MonoBehaviour
{
    public static LevelGameManager Instance { get; private set; }
    private bool isPaused = false;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Update()
    {
        // 關卡邏輯（例如 check win/lose），但不要處理 ESC
    }

    // 被 GlobalGameManager 呼叫
    public void PauseGame()
    {
        if (isPaused) return;
        isPaused = true;
        // 關卡專屬暫停行為：停止 AI、計時器、動畫、particle 等
        PauseAllEnemies();
        // 顯示暫停 UI（透過 SceneUIManager）
        var sceneUI = UIRegistry.Instance?.GetCurrent();
        sceneUI?.OpenPause();
    }

    public void ResumeGame()
    {
        if (!isPaused) return;
        isPaused = false;
        // 恢復關卡專屬行為
        ResumeAllEnemies();
        // 關閉暫停 UI（若需要）
        var pause = FindObjectOfType<PausePanel>();
        if (pause != null) pause.Close();
    }

    void PauseAllEnemies()
    {
        // 範例：找到所有 enemy 並暫停 AI（實作取決於專案）
        // foreach (var e in FindObjectsOfType<Enemy>()) e.Pause();
    }

    void ResumeAllEnemies()
    {
        // foreach (var e in FindObjectsOfType<Enemy>()) e.Resume();
    }
}
