using UnityEngine;
using TMPro;

public class EnemyHealthDisplay : MonoBehaviour
{
    public RectTransform enemyFront;     // Green health bar that shrinks
    public TextMeshProUGUI healthText;  // Optional: Health text display (e.g., "50/100")
    public EnemyStats enemyStats;      // Reference to the enemy's stats

    private float originalWidth;        // Stores the initial width of the green bar

    private void Awake()
    {
        // Get the EnemyStats component on the same GameObject
        enemyStats = GetComponent<EnemyStats>();

        if (enemyStats == null)
        {
            Debug.LogError($"EnemyStats component not found on {gameObject.name}");
        }

        // Ensure enemyFront (the health bar) is assigned
        if (enemyFront != null)
        {
            originalWidth = enemyFront.sizeDelta.x; // Store the initial width of the health bar
        }
        else
        {
            Debug.LogError($"EnemyFront RectTransform not assigned on {gameObject.name}");
        }

    }

    private void Start()
    {
        // Ensure the health bar is updated after EnemyStats is initialized
        UpdateHealthBar();
    }


    // Updates the health bar and health text
    public void UpdateHealthBar()
    {
        if (enemyFront != null && enemyStats != null)
        {
            // Calculate health percentage
            float healthPercentage = (float)enemyStats.currentHealth / enemyStats.maxHealth;

            // Debug the health calculation
            Debug.Log($"Updating Health Bar: {gameObject.name}, Current Health: {enemyStats.currentHealth}/{enemyStats.maxHealth}, Percentage: {healthPercentage}");

            // Adjust the width of the green bar
            enemyFront.sizeDelta = new Vector2(originalWidth * healthPercentage, enemyFront.sizeDelta.y);

            // Update health text if assigned
            if (healthText != null)
            {
                healthText.text = $"{enemyStats.currentHealth}/{enemyStats.maxHealth}";
            }
        }
        else
        {
            Debug.LogError($"Missing EnemyStats or EnemyFront for {gameObject.name}");
        }
    }
}
