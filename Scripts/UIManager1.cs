// using UnityEngine;

// public class UIManager1 : MonoBehaviour
// {
//     public static UIManager1 Instance;

//     [Header("Panels")]
//     public GameObject mainMenuPanel;
//     public GameObject settingPanel;
//     public GameObject levelSelectPanel;
//     public GameObject hudPanel;
//     public GameObject pausePanel;
//     public GameObject resultPanel;

//     // 啟動時呼叫，確保唯一
//     void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//         Debug.Log("candy");
//         HideAll();
//     }

//     public void ShowPanel(GameObject panel)
//     {
//         HideAll();
//         panel.SetActive(true);
//     }

//     public void HideAll()
//     {
//         mainMenuPanel.SetActive(false);
//         settingPanel.SetActive(false);
//         levelSelectPanel.SetActive(false);
//         hudPanel.SetActive(false);
//         pausePanel.SetActive(false);
//         resultPanel.SetActive(false);
//     }
// }
