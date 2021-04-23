using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    private float maxHealth = 100f;

    public void SetHealth(float health) {
        healthBar.value = health / maxHealth;
    }

    public void SetMaxHealth(float maxHealth) {
        this.maxHealth = maxHealth;
    }
}
