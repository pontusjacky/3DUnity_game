using UnityEngine;

public class UIRegistry : MonoBehaviour
{
    public static UIRegistry Instance { get; private set; }

    private SceneUIManager currentSceneUI;

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

    public void RegisterSceneUI(SceneUIManager sceneUI)
    {
        currentSceneUI = sceneUI;
    }

    public void UnregisterSceneUI(SceneUIManager sceneUI)
    {
        if (currentSceneUI == sceneUI) currentSceneUI = null;
    }

    public SceneUIManager GetCurrent() => currentSceneUI;
}
