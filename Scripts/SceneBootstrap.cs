using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneBootstrap : MonoBehaviour
{
    public static SceneBootstrap Instance { get; private set; }
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

    IEnumerator Start()
    {
        yield return null;
        yield return SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }
}
