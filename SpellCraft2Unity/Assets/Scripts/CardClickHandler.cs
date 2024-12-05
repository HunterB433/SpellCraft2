using UnityEngine;

public class CardClickHandler : MonoBehaviour
{
    private SpellComponent spellComponent; // Spell attributes (damage, type, etc.)
    private EnemyStats enemyStats;         // Reference to the enemy's stats
    private PlayerStats playerStats;       // Reference to the player's stats

    private void Start()
    {
        // Get the SpellComponent attached to this card
        spellComponent = GetComponent<SpellComponent>();

        if (spellComponent == null)
        {
            Debug.LogError("No SpellComponent found on this GameObject!");
        }

        // Find the enemy and player stats in the scene
        enemyStats = FindObjectOfType<EnemyStats>();
        playerStats = FindObjectOfType<PlayerStats>();

        if (enemyStats == null)
        {
            Debug.LogError("EnemyStats not found in the scene!");
        }

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats not found in the scene!");
        }
    }

    private void OnMouseDown()
    {
        if (spellComponent != null && enemyStats != null && playerStats != null)
        {
            // Player attacks the enemy with a spell
            enemyStats.TakeDamage(spellComponent.power, spellComponent.typeChar);

            // If the enemy is alive, it attacks back
            if (enemyStats.currentHealth > 0)
            {
                // Generate random damage
                int damage = enemyStats.GetRandomAttackDamage();
                // Apply damage to the player
                playerStats.TakeDamage(damage);
                Debug.Log($"Enemy attacked back, dealing {damage} damage to the player!");
            }
        }
    }
}
