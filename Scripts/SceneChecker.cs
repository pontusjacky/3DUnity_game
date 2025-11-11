using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChecker : MonoBehaviour
{
    void Start()
    {
        Debug.Log("=== Loaded Scenes ===");
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene s = SceneManager.GetSceneAt(i);
            Debug.Log($"[{i}] {s.name}, loaded={s.isLoaded}");
        }
    }
}