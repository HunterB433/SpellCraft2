using TMPro; // Add for TextMeshPro support
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 10;          // Enemy's maximum health
    public int currentHealth;          // Enemy's current health
    public int minAttackDamage = 2;    // Minimum damage the enemy can deal
    public int maxAttackDamage = 4;    // Maximum damage the enemy can deal

    [SerializeField]
    private char enemyType;           // Type of the enemy ('F' = Fire, 'E' = Earth, 'W' = Water)

    [SerializeField]
    private TextMeshProUGUI damageText; // Reference to the UI Text for displaying damage

    private void Awake()
    {
        // Initialize current health to the maximum
        currentHealth = maxHealth;

        // Update the damage display at startup
        UpdateDamageText();
    }

    public char GetEnemyType()
    {
        return enemyType; // Getter method for enemyType
    }

    public int GetRandomAttackDamage()
    {
        // Generate a random damage value between minAttackDamage and maxAttackDamage
        return Random.Range(minAttackDamage, maxAttackDamage + 1);
    }

    public void TakeDamage(int damage, char spellType)
    {
        // Check for elemental weakness and apply multiplier
        float multiplier = GetDamageMultiplier(spellType);

        // Apply buff if the enemy is hit by their own element
        if (enemyType == spellType)
        {
            IncrementDamage(); // Increment the damage
        }

        // Calculate final damage
        int finalDamage = Mathf.RoundToInt(damage * multiplier);
        currentHealth -= finalDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{gameObject.name} took {finalDamage} damage (x{multiplier} multiplier). Current HP: {currentHealth}");

        // Update health bar (if assigned)
        EnemyHealthDisplay healthDisplay = GetComponent<EnemyHealthDisplay>();
        if (healthDisplay != null)
        {
            healthDisplay.UpdateHealthBar();
        }

        // Check if the enemy is defeated
        if (currentHealth <= 0)
        {
            Die();
            UnlockNewLevel();

            // If the current scene is the chaos rat scene
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                SceneManager.LoadScene("Win");
            }
            else
            {
                SceneManager.LoadScene("Dungeon");
            }
        }
    }

    private void IncrementDamage()
    {
        // Increment minimum attack damage by 2
        minAttackDamage += 2;
        // Increment maximum attack damage by 2
        maxAttackDamage += 2;

        Debug.Log($"{gameObject.name}'s attack damage was increased! New Damage Range: {minAttackDamage} - {maxAttackDamage}");

        // Update the damage display
        UpdateDamageText();
    }

    private void UpdateDamageText()
    {
        if (damageText != null)
        {
            damageText.text = $"Damage: {minAttackDamage} - {maxAttackDamage}";
        }
        else
        {
            Debug.LogWarning("Damage Text is not assigned!");
        }
    }

    private float GetDamageMultiplier(char spellType)
    {
        Debug.Log($"Calculating multiplier: EnemyType={enemyType}, SpellType={spellType}");

        // Define elemental weaknesses
        if ((enemyType == 'I' && spellType == 'M') ||  // Magma > Ice
            (enemyType == 'E' && spellType == 'I') ||  // Ice > Earth
            (enemyType == 'C' && spellType == 'E') ||  // Earth > Chaos
            (enemyType == 'M' && spellType == 'C'))    // Chaos > Fire
        {
            Debug.Log("Weakness hit! Multiplier: 1.5");
            // Weakness multiplier
            return 1.5f;
        }

        //if the spell type is the same as the enemy attacks only deal 50% damage
        else if ((enemyType == 'I' && spellType == 'I') ||  
                 (enemyType == 'E' && spellType == 'E') ||  
                 (enemyType == 'M' && spellType == 'M') ||
                 (enemyType == 'C' && spellType == 'C'))
        {
            Debug.Log("Ineffective hit, 50% damage");
            // Ineffective multiplier
            return 0.5f;
        }

        Debug.Log("No weakness. Multiplier: 1.0");
        // Default multiplier
        return 1.0f;
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has been defeated!");
        gameObject.SetActive(false);
    }

    private void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
