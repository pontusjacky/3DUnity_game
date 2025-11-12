using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [Header("Config")]
    [Tooltip("Optional: Loading screen canvas/prefab to show while loading. Can be null.")]
    public GameObject loadingScreenPrefab;

    // Events
    public event Action<string> OnSceneStartLoading;   // param: sceneName
    public event Action<string> OnSceneLoaded;         // param: sceneName
    public event Action<string> OnSceneUnloaded;       // param: sceneName
    public event Action<float> OnLoadProgress;         // param: 0..1 progress

    GameObject loadingScreenInstance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #region Public API

    /// <summary>
    /// 非同步載入場景（Single 或 Additive）
    /// </summary>
    public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        StartCoroutine(LoadSceneRoutine(sceneName, mode));
    }

    /// <summary>
    /// 非同步卸載 Additive 場景
    /// </summary>
    public void UnloadScene(string sceneName)
    {
        StartCoroutine(UnloadSceneRoutine(sceneName));
    }

    #endregion

    #region Routines

    IEnumerator LoadSceneRoutine(string sceneName, LoadSceneMode mode)
    {
        // 檢查場景是否在 Build Settings
        if (!IsSceneInBuild(sceneName))
        {
            Debug.LogError($"[SceneLoader] Scene '{sceneName}' not found in Build Settings.");
            yield break;
        }

        OnSceneStartLoading?.Invoke(sceneName);

        // 顯示 loading 畫面（如果有設定）
        ShowLoadingScreen();

        var asyncOp = SceneManager.LoadSceneAsync(sceneName, mode);
        // allowSceneActivation=true 預設為 true，若要在 0.9 -> 等待時用 false 控制 activation 可另實作
        asyncOp.allowSceneActivation = true;

        // 當 mode == Single，Unity 會自動 unload 其他非 additive 場景
        while (!asyncOp.isDone)
        {
            // 注意：progress 在 0~0.9 之間（activation 前），最後會到 1.0
            float progress = Mathf.Clamp01(asyncOp.progress / 0.9f);
            OnLoadProgress?.Invoke(progress);
            OnLoadProgress_Internal(progress);
            yield return null;
        }

        // 等 Unity 完成場景切換後，做一些額外處理（例如處理 AudioListener）
        HandlePostLoad(sceneName, mode);

        OnSceneLoaded?.Invoke(sceneName);

        // 隱藏 loading 畫面（可以等動畫）
        HideLoadingScreen();
    }

    IEnumerator UnloadSceneRoutine(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).IsValid())
        {
            Debug.LogWarning($"[SceneLoader] Scene '{sceneName}' is not currently loaded.");
            yield break;
        }

        var op = SceneManager.UnloadSceneAsync(sceneName);
        while (!op.isDone) yield return null;

        OnSceneUnloaded?.Invoke(sceneName);
    }

    #endregion

    #region Helpers

    bool IsSceneInBuild(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == sceneName) return true;
        }
        return false;
    }

    void ShowLoadingScreen()
    {
        if (loadingScreenPrefab == null) return;
        if (loadingScreenInstance == null)
        {
            loadingScreenInstance = Instantiate(loadingScreenPrefab);
            DontDestroyOnLoad(loadingScreenInstance);
        }
        loadingScreenInstance.SetActive(true);

        // 如果 loading screen 有一個可接收進度的 component，可以把事件綁上去
        var uiProgress = loadingScreenInstance.GetComponent<ILoadingScreen>();
        if (uiProgress != null)
            OnLoadProgress += uiProgress.SetProgress;
    }

    void HideLoadingScreen()
    {
        if (loadingScreenInstance == null) return;
        var uiProgress = loadingScreenInstance.GetComponent<ILoadingScreen>();
        if (uiProgress != null)
            OnLoadProgress -= uiProgress.SetProgress;

        loadingScreenInstance.SetActive(false);
    }

    void OnLoadProgress_Internal(float v)
    {
        // 內部用，若需 debug 可在此處統一處理
    }

    void HandlePostLoad(string sceneName, LoadSceneMode mode)
    {
        // 範例：避免場景重複的 AudioListener，或把主相機的 AudioListener 啟用
        // 若你有一個全域 AudioManager（DontDestroyOnLoad），則場景內的 AudioListener 應該關閉
        CleanupAudioListeners();
    }

    void CleanupAudioListeners()
    {
        var listeners = FindObjectsOfType<UnityEngine.AudioListener>();
        if (listeners.Length <= 1) return;

        // 保留第一個（或依你需求選擇），禁用其他的
        bool keptOne = false;
        foreach (var l in listeners)
        {
            if (!keptOne)
            {
                keptOne = true;
                continue;
            }
            // 如果是場景相機的 listener，通常禁用 component 比 Destroy 更安全
            l.enabled = false;
        }
    }

    #endregion
}
