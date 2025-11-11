using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("場景用 UI 元素")]
    public Transform canvas;
    public Transform popupRoot;
    public LevelSelectPanel levelSelectPrefab;
    public LevelSelectPanel levelSelectInstance;

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

    public void OpenLevelSelect()
    {
        if (levelSelectInstance == null)
        {
            levelSelectInstance = Instantiate(levelSelectPrefab, canvas);
            levelSelectInstance.Open();
        }

        levelSelectInstance.Open();
    }

}
