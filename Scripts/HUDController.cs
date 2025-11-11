using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class HUDController : MonoBehaviour
{
    [Header("References (bind in Inspector)")]
    public Slider hpSlider;
    public TMP_Text hpText;
    public Slider staminaSlider;
    public TMP_Text staminaText;
    public Slider xpSlider;
    public TMP_Text levelText;

    // runtime
    private PlayerStats currentPlayerStats;


    void Start()
    {
        TryBindToPlayer();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryBindToPlayer();
    }

    void TryBindToPlayer()
    {
        if (currentPlayerStats != null)
            UnsubscribeFromPlayer(currentPlayerStats);

        PlayerStats found = null;

        var t = GameObject.FindWithTag("Player");
        if (t != null)
            found = t.GetComponent<PlayerStats>();

        if (found == null)
            found = FindObjectOfType<PlayerStats>();

        if (found != null)
        {
            BindToPlayer(found);
            Debug.Log($"HUD bound to player: {found.gameObject.name}");
        }
        else
        {
            Debug.Log("HUDController: no PlayerStats found in scene.");
            ClearUI();
        }
    }

    void BindToPlayer(PlayerStats ps)
    {
        currentPlayerStats = ps;
        UpdateHpUI(ps.currentHp, ps.maxHp);
        UpdateStaminaUI(ps.currentStamina, ps.maxStamina);
        UpdateXPUI(ps.currentXP, ps.xpToNextLevel);
        if (levelText) levelText.text = "Lv " + ps.level;

        ps.OnHpChanged += UpdateHpUI;
        ps.OnStaminaChanged += UpdateStaminaUI;
        ps.OnXPChanged += UpdateXPUI;
        ps.OnLevelUp += OnLevelUp;
        ps.OnDamageTaken += ShowDamage;
    }

    void UnsubscribeFromPlayer(PlayerStats ps)
    {
        ps.OnHpChanged -= UpdateHpUI;
        ps.OnStaminaChanged -= UpdateStaminaUI;
        ps.OnXPChanged -= UpdateXPUI;
        ps.OnLevelUp -= OnLevelUp;
        ps.OnDamageTaken -= ShowDamage;
    }

    void ClearUI()
    {
        if (hpSlider) { hpSlider.maxValue = 1; hpSlider.value = 0; }
        if (staminaSlider) { staminaSlider.maxValue = 1; staminaSlider.value = 0; }
        if (xpSlider) { xpSlider.maxValue = 1; xpSlider.value = 0; }
        if (hpText) hpText.text = "-";
        if (staminaText) staminaText.text = "-";
        if (levelText) levelText.text = "";
    }

    void UpdateHpUI(int cur, int max)
    {
        if (hpSlider) { hpSlider.maxValue = max; hpSlider.value = cur; }
        if (hpText) hpText.text = $"{cur} / {max}";
    }

    void UpdateStaminaUI(int cur, int max)
    {
        if (staminaSlider) { staminaSlider.maxValue = max; staminaSlider.value = cur; }
        if (staminaText) staminaText.text = $"{cur} / {max}";
    }

    void UpdateXPUI(int cur, int needed)
    {
        if (xpSlider) { xpSlider.maxValue = needed; xpSlider.value = cur; }
    }

    void OnLevelUp(int newLevel)
    {
        if (levelText) levelText.text = "Lv " + newLevel;
    }

    void ShowDamage(int dmg)
    {
        Debug.Log($"Damage popup: {dmg}");
    }
}
