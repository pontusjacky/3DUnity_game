using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public int maxHp = 100;
    public int maxStamina = 100;
    public int level = 1;
    public int currentHp;
    public int currentStamina;
    public int currentXP;
    public int xpToNextLevel = 100;

    public event Action<int,int> OnHpChanged;         // (current, max)
    public event Action<int,int> OnStaminaChanged;    // (current, max)
    public event Action<int,int> OnXPChanged;         // (current, xpToNext)
    public event Action<int> OnDamageTaken;           // damage amount
    public event Action<int> OnLevelUp;               // new level

    void Awake()
    {
        currentHp = maxHp;
        currentStamina = maxStamina;
        currentXP = 0;
    }

    public void TakeDamage(int dmg)
    {
        currentHp = Mathf.Max(0, currentHp - dmg);
        OnHpChanged?.Invoke(currentHp, maxHp);
        OnDamageTaken?.Invoke(dmg);

        if (currentHp == 0)
            Die();
    }

    public void Heal(int amount)
    {
        currentHp = Mathf.Min(maxHp, currentHp + amount);
        OnHpChanged?.Invoke(currentHp, maxHp);
    }

    public void UseStamina(int amount)
    {
        currentStamina = Mathf.Max(0, currentStamina - amount);
        OnStaminaChanged?.Invoke(currentStamina, maxStamina);
    }

    public void RecoverStamina(int amount)
    {
        currentStamina = Mathf.Min(maxStamina, currentStamina + amount);
        OnStaminaChanged?.Invoke(currentStamina, maxStamina);
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            level++;
            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.2f);
            OnLevelUp?.Invoke(level);
        }
        OnXPChanged?.Invoke(currentXP, xpToNextLevel);
    }

    void Die()
    {
        Debug.Log("Player died");
        // 播放死亡動畫、禁用控制
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) TakeDamage(UnityEngine.Random.Range(5,20));
        if (Input.GetKeyDown(KeyCode.L)) AddXP(UnityEngine.Random.Range(10,50));
        if (Input.GetKeyDown(KeyCode.J)) UseStamina(10);
        if (Input.GetKeyDown(KeyCode.H)) Heal(10);
    }
}
