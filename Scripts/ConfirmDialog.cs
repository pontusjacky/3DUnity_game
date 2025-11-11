using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ConfirmDialog : MonoBehaviour
{
    public TMP_Text msgText;
    public Button yesBtn;
    public Button noBtn;

    public void Setup(string msg, Action onYes)
    {
        msgText.text = msg;
        yesBtn.onClick.AddListener(() => { onYes?.Invoke(); Destroy(gameObject); });
        noBtn.onClick.AddListener(() => Destroy(gameObject));
    }
}
