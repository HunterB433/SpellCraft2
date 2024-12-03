using UnityEngine;
using TMPro; // Include this for TextMeshPro support

public class SpellComponent : MonoBehaviour
{
    public int power;
    public char typeChar;

    public void SetSpellData(SpellManager.Spell spell)
    {
        // Set internal data
        // For later logic
        power = spell.power;
        typeChar = (char)spell.type;

        // Find child objects and update TextMeshPro components
        // We arent changing the image, just need it to get the children
        Transform imageTransform = transform.Find("Image");

        if (imageTransform != null)
        {
            // Update Damage text
            // Usuall flow will be
                // Find item by name (can be optimized later)
                // Check if found
                // Replace
            Transform damageTransform = imageTransform.Find("Damage");
            if (damageTransform != null)
            {
                TextMeshPro textDamage = damageTransform.GetComponent<TextMeshPro>();
                if (textDamage != null)
                {
                    textDamage.text = $"{power}";
                }
            }

            // Update Type text
            // Same as above
            Transform typeTransform = imageTransform.Find("Type");
            if (typeTransform != null)
            {
                TextMeshPro textType = typeTransform.GetComponent<TextMeshPro>();
                if (textType != null)
                {
                    textType.text = $"{typeChar}";
                }
            }
        }
        else
        {
            Debug.LogError("Prefab is missing 'Image' child!, Cannot continue");
        }

        Debug.Log($"Assigned A Spell - Power: {power}, element: {typeChar}");
    }
}
