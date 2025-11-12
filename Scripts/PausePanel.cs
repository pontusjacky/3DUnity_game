using UnityEngine;
using UnityEngine.UI;

public class PausePanel : PanelBase
{

    [Header("Button")]
    public Button backButton;
    public Button settingsButton;
    public Button menuButton;


    void Start()
    {
        backButton.onClick.AddListener(() => back());
        // settingsButton.onClick.AddListener(() =>
        // {
        //     var popup = PopupManager.Instance;
        //     popup.ShowConfirm("開啟設定？", () => Debug.Log("確定開啟設定"));
        // });
        settingsButton.onClick.AddListener(() => openSetting());
        menuButton.onClick.AddListener(() => backToMainMenu());
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
    public void backToMainMenu()
    {
        GameManager.Instance.Resume();
        GameManager.Instance.GoToMainMenu();
    }
    public void openSetting()
    {
        var sceneUI = UIRegistry.Instance?.GetCurrent();
        sceneUI?.OpenSetting();
    }

}
