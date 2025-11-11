using UnityEngine;
using UnityEngine.UI;

public class LevelSelectPanel : PanelBase
{

    [Header("Button")]
    public Button settingsButton;
    public Button backButton;
 

    void Start()
    {
        backButton.onClick.AddListener(() => back());
        settingsButton.onClick.AddListener(() =>
        {
            var popup = PopupManager.Instance;
            popup.ShowConfirm("開啟設定？", () => Debug.Log("確定開啟設定"));
        });
    }

    public override void Open()
    {
        base.Open();
        Time.timeScale = 0f;
    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 1f;
    }

    public void back()
    {
        GameManager.Instance.Resume();
    }
}
