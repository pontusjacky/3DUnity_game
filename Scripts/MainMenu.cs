using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Button")]
    public Button startButton;
    public Button settingButton;
    public Button quitButton;

    void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(StartClicked);
        if (startButton != null)
            settingButton.onClick.AddListener(settingClicked);
        if (startButton != null)
            quitButton.onClick.AddListener(quitClicked);

    }

    public void StartClicked()
    {
        GameManager.Instance?.QuitGame();
    }
    public void settingClicked()
    {
        GameManager.Instance?.QuitGame();
    }
    public void quitClicked()
    {
        GameManager.Instance?.QuitGame();
    }
    

    // public void StartGame()
    // {
    //     UIManager.Instance.ShowPanel(UIManager.Instance.levelSelectPanel);
    // }

    // public void OpenSettings()
    // {
    //     UIManager.Instance.ShowPanel(UIManager.Instance.settingPanel);
    // }
    // public void QuitGame()
    // {
    //     #if UNITY_EDITOR
    //         UnityEditor.EditorApplication.isPlaying = false;
    //     #else
    //         Application.Quit();
    //     #endif
    // }
}
