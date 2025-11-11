using UnityEngine;
using System;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public float MasterVolume { get; private set; } = 1f;
    public int GraphicsQuality { get; private set; } = 2;

    public static event Action OnSettingsChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void SetMasterVolume(float v)
    {
        MasterVolume = Mathf.Clamp01(v);
        AudioListener.volume = MasterVolume;
        Save();
        OnSettingsChanged?.Invoke();
    }

    public void SetQuality(int q)
    {
        GraphicsQuality = q;
        QualitySettings.SetQualityLevel(q);
        Save();
        OnSettingsChanged?.Invoke();
    }

    void Load()
    {
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        GraphicsQuality = PlayerPrefs.GetInt("GraphicsQuality", 2);
        AudioListener.volume = MasterVolume;
        QualitySettings.SetQualityLevel(GraphicsQuality);
    }

    void Save()
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
        PlayerPrefs.SetInt("GraphicsQuality", GraphicsQuality);
        PlayerPrefs.Save();
    }
}
