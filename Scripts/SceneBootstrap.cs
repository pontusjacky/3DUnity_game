using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneBootstrap : MonoBehaviour
{
    // private IEnumerator Start()
    // {
    //     yield return SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
    //     yield return null;
    //     UIManager.Instance.ShowPanel(UIManager.Instance.mainMenuPanel);
    //     // yield return SceneManager.LoadSceneAsync("mini_exercise1", LoadSceneMode.Additive);

    //     SceneManager.UnloadSceneAsync("Bootstrap");
    // }

    IEnumerator Start()
    {
        yield return null;
        yield return SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }
}
