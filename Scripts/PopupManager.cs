using UnityEngine;
using System;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    public GameObject confirmPrefab;

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

    public void ShowConfirm(string msg, Action onYes)
    {
        var sceneUI = UIRegistry.Instance?.GetCurrent();
        if (sceneUI != null && confirmPrefab != null)
        {
            var go = Instantiate(confirmPrefab, sceneUI.popupRoot);
            var confirm = go.GetComponent<ConfirmDialog>();
            confirm.Setup(msg, onYes);
        }
    }
}
