using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static UnityEditor.LightingExplorerTableColumn;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 10;          // Enemy's maximum health
    public int currentHealth;           // Enemy's current health
    public int minAttackDamage = 2;    // Minimum damage the enemy can deal
    public int maxAttackDamage = 4;    // Maximum damage the enemy can deal

    private void Awake()
    {
        // Initialize current health to the maximum
        currentHealth = maxHealth;
    }

    public int GetRandomAttackDamage()
    {
        // Generate a random damage value between minAttackDamage and maxAttackDamage
        return Random.Range(minAttackDamage, maxAttackDamage + 1);
    }

    public void TakeDamage(int damage)
    {
        // Reduce current health by the damage amount
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{gameObject.name} took {damage} damage. Current HP: {currentHealth}");

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

            //if the current scene is the chaos rat scene
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                SceneManager.LoadScene("Win");
            }
            else
            {
                //return to the dungeon scene
                SceneManager.LoadScene("Dungeon");
            }
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has been defeated!");


        // ====================================== IMPORTANT ======================================
        // Temporarily disables the enemy, but we should send the user back to the dugeon scene when enemy is defeated
        gameObject.SetActive(false); 
    }

    //this function is for the level unlocker
    void UnlockNewLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
