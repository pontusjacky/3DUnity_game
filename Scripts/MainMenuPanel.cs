using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : PanelBase
{

    [Header("Button")]
    public Button startButton;
    public Button settingButton;
    public Button quitButton;

    protected override void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }
    void Start()
    {
        startButton.onClick.AddListener(() => StartClicked());
        settingButton.onClick.AddListener(() => settingClicked());
        quitButton.onClick.AddListener(() => quitClicked());
    }

    public void StartClicked()
    {
        GameManager.Instance?.GoToLevelSelect();
    }
    public void settingClicked()
    {
        GameManager.Instance?.QuitGame();
    }
    public void quitClicked()
    {
        GameManager.Instance?.QuitGame();
    }
}
