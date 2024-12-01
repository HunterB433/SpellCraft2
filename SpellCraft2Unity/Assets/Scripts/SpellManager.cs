using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [System.Serializable]
    public struct Spell
    {
        public Sprite image;
        public int power;
        public SpellType type;

        public Spell(Sprite image, int power, SpellType type)
        {
            this.image = image;
            this.power = power;
            this.type = type;
        }
    }

    public enum SpellType
    {
        Magma = 'M',
        Ice = 'I',
        Chaos = 'C',
        Earth = 'E'
    }

    public List<Spell> spells = new List<Spell>();
    public GameObject spellPrefab; // Assign prefab in the Unity Inspector
    
    public Vector3 spawnOffset = new Vector3(2, 0, 0); // Offset between spawned spells
    public Vector3 spawnStartPosition = new Vector3(0, 0, -1); // Starting position for the spells

    void Start()
    {
        GenerateSpellPrefabs();
    }

    private void GenerateSpellPrefabs()
    {
        if (spellPrefab == null)
        {
            Debug.LogError("Spell prefab is not assigned!");
            return;
        }

        Vector3 currentPosition = spawnStartPosition;

        foreach (Spell spell in spells)
        {
            // Instantiate the prefab as a 3D GameObject
            GameObject spellInstance = Instantiate(spellPrefab, currentPosition, Quaternion.identity);

            // Assign sprite to the SpriteRenderer (if the prefab has one)
            SpriteRenderer spriteRenderer = spellInstance.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = spell.image;
            }
            else
            {
                Debug.LogWarning("No SpriteRenderer found in prefab!");
            }

            // Assign type and power values to custom components (if needed)
            SpellComponent spellComponent = spellInstance.GetComponent<SpellComponent>();
            if (spellComponent != null)
            {
                spellComponent.SetSpellData(spell);
            }

            // Update the position for the next spell
            currentPosition += spawnOffset;
        }
    }
}
