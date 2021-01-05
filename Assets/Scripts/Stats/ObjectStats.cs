using UnityEngine;
using UnityEngine.UI;

public class ObjectStats : MonoBehaviour {
    [Header("Stats")]
    public Stat health;
    public Stat movementSpeed;
    public Stat damage;

    public int currentHealth { get; private set; }

    private Healthbar healthbar;
    void Awake() {
        currentHealth = health.GetValue();
        healthbar = GetComponentInChildren<Healthbar>();
        if(healthbar) {
            healthbar.Initialize(health.GetValue());
        }
    }

    public void Start() {}

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        Mathf.Clamp(currentHealth, 0, health.GetValue());

        if(healthbar) {
            healthbar.UpdateHealthBar(currentHealth);
        }

        if(currentHealth <= 0) {
            Die();
        }
    }

    public void Heal(int amout) {
        currentHealth += amout;
        Mathf.Clamp(currentHealth, 0, health.GetValue());
        if(healthbar) {
            healthbar.UpdateHealthBar(currentHealth);
        }
    }

    public void ApplyEffect(AbilityEffect effect) {
        StartCoroutine(effect.Apply(this));
    }

    public virtual void Die() {
        // Play VFX
        // Play SFX
        Destroy(gameObject);
    }
}
