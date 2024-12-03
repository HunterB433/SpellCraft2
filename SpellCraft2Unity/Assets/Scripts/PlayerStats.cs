using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 20;          // Maximum player health
    public int currentHealth;           // Current player health
    public RectTransform healthBar;     // Health bar (green rectangle)
    public TextMeshProUGUI healthText;  // Health text (optional)

    private float originalWidth;        // Initial width of the health bar

    private void Start()
    {
        // Initialize player health
        currentHealth = maxHealth;

        // Store the initial width of the health bar
        if (healthBar != null)
            originalWidth = healthBar.sizeDelta.x;

        // Update health display
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        // Reduce health
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Player took {damage} damage. Current Health: {currentHealth}");

        // Update health bar
        UpdateHealthBar();

        // Check if the player is defeated
        if (currentHealth <= 0)
        {
            Debug.Log("Player defeated!");
            // ===================================== IMPORTANT =====================================
            //We could add a pop that displays a game over message with a bottom that sends teh user to scene 0

            SceneManager.LoadScene("Dungeon");
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            // Calculate health percentage
            float healthPercentage = (float)currentHealth / maxHealth;

            // Adjust the width of the health bar
            healthBar.sizeDelta = new Vector2(originalWidth * healthPercentage, healthBar.sizeDelta.y);
        }

        // Update health text
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }
}
