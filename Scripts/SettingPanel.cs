using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingPanel : PanelBase
{
    public Slider volumeSlider;
    public TMP_Dropdown qualityDropdown;
    public Button closeButton;

    void Start()
    {
        var setting = SettingsManager.Instance;
        volumeSlider.value = setting.MasterVolume;
        qualityDropdown.value = setting.GraphicsQuality;

        volumeSlider.onValueChanged.AddListener(v => setting.SetMasterVolume(v));
        qualityDropdown.onValueChanged.AddListener(i => setting.SetQuality(i));
        closeButton.onClick.AddListener(() => Close());
    }

    public override void Open()
    {
        base.Open();
        var setting = SettingsManager.Instance;
        volumeSlider.value = setting.MasterVolume;
        qualityDropdown.value = setting.GraphicsQuality;
    }
}
