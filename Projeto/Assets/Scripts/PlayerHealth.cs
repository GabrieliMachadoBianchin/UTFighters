using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("UI (opcional)")]
    [SerializeField] private Slider healthBar;

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    public event Action OnDeath;
    public event Action<int, int> OnHealthChanged; // (atual, máximo)

    private bool isDead;

    // Chamado pelo MatchManager no início da partida, com o maxHealth do CharacterData.
    public void Setup(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        isDead = false;

        if (healthBar != null)
        {
            healthBar.maxValue = MaxHealth;
            healthBar.value = CurrentHealth;
        }

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);

        if (healthBar != null)
            healthBar.value = CurrentHealth;

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0)
        {
            isDead = true;
            OnDeath?.Invoke();
        }
    }
}
