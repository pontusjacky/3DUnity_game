using UnityEngine;
using UnityEngine.UI;

public class LevelSelectPanel : PanelBase
{

    [Header("關卡數量")]
    public int level_num;
    [Header("Button")]
    public Button settingsButton;
    public Button backButton;
    public Button levelButtonPrefab;
    [Header("關卡按鈕目錄")]
    public Transform levelGrid;


 
    protected override void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        for( int i = 0; i < level_num; i++)
        {
            int index = i + 1;
            var levelButton = Instantiate(levelButtonPrefab, levelGrid);
            levelButton.onClick.AddListener(() => goToLevel(index));
        }
            
        backButton.onClick.AddListener(() => back());
    }
    public void back()
    {
        GameManager.Instance.GoToMainMenu();
    }

    public void goToLevel(int i)
    {
        GameManager.Instance.GoToLevel(i);
    }
}
