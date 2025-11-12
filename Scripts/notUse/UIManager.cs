using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("場景用 UI 元素")]
    public Transform canvas;
    public Transform popupRoot;
    public LevelSelectPanel levelSelectPrefab;
    public LevelSelectPanel levelSelectInstance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
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
