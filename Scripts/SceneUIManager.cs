using UnityEngine;

public class SceneUIManager : MonoBehaviour
{
    [Header("場景用 UI 元素")]
    public Transform UIRoot;
    public Transform popupRoot;
    public PausePanel pausePanelPrefab;

    private PausePanel pausePanelInstance;
    void Awake()
    {
        // 註冊自己到全域
        if (UIRegistry.Instance != null)
            UIRegistry.Instance.RegisterSceneUI(this);
    }

    void OnDestroy()
    {
        // 當場景卸載時自動取消登記
        if (UIRegistry.Instance != null)
            UIRegistry.Instance.UnregisterSceneUI(this);
    }

    public void OpenPause()
    {
        if (pausePanelInstance == null)
        {
            pausePanelInstance = Instantiate(pausePanelPrefab, UIRoot);
            pausePanelInstance.Open();
        }

        pausePanelInstance.Open();
    }

}
