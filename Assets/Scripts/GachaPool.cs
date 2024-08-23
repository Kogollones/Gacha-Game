using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Gacha Pool", menuName = "Gacha/Gacha Pool")]
public class GachaPool : ScriptableObject
{
    public int costPerPull = 100; 

    public List<GachaItem> items = new List<GachaItem>
    {
        // Personajes
        new GachaItem { name = "Anya", rarity = 3, probability = 0.05f, itemType = InventoryItemType.Character },
        new GachaItem { name = "Kael", rarity = 4, probability = 0.02f, itemType = InventoryItemType.Character },
        new GachaItem { name = "Lyra", rarity = 2, probability = 0.1f, itemType = InventoryItemType.Character },
        new GachaItem { name = "Grom", rarity = 1, probability = 0.3f, itemType = InventoryItemType.Character },
        new GachaItem { name = "Luna", rarity = 5, probability = 0.01f, itemType = InventoryItemType.Character },

        // Armas
        new GachaItem { name = "Espada de Hierro", rarity = 2, probability = 0.2f, itemType = InventoryItemType.Weapon },
        new GachaItem { name = "Arco Élfico", rarity = 3, probability = 0.1f, itemType = InventoryItemType.Weapon },
        new GachaItem { name = "Bastón Mágico", rarity = 4, probability = 0.05f, itemType = InventoryItemType.Weapon },
        new GachaItem { name = "Hacha de Guerra", rarity = 1, probability = 0.3f, itemType = InventoryItemType.Weapon },
        new GachaItem { name = "Dagas Gemelas", rarity = 5, probability = 0.01f, itemType = InventoryItemType.Weapon },

        // Consumibles
        new GachaItem { name = "Poción de Curación", rarity = 1, probability = 0.4f, itemType = InventoryItemType.Consumable },
        new GachaItem { name = "Pergamino de Sabiduría", rarity = 2, probability = 0.2f, itemType = InventoryItemType.Consumable },
        new GachaItem { name = "Gema de Poder", rarity = 3, probability = 0.08f, itemType = InventoryItemType.Consumable }
        // ... puedes añadir más objetos aquí
    };

    // Realiza una tirada gacha
    public GachaItem PullGacha()
    {
        float randomValue = Random.value;
        float cumulativeProbability = 0f;

        foreach (GachaItem item in items)
        {
            cumulativeProbability += item.probability;
            if (randomValue <= cumulativeProbability)
            {
                return item; 
            }
        }

        Debug.LogError("Error en el sistema de gacha. Verificar las probabilidades.");
        return items[0]; 
    }
}

// Representa un objeto individual dentro del gacha
[System.Serializable]
public class GachaItem : Item 
{
    // No es necesario redefinir name, rarity, icon, ni itemType aquí
    // Puedes añadir tus propiedades de GachaItem específicas si es necesario
}

// Enumeración para los tipos de objetos
public enum ItemType
{
    Character,
    Weapon,
    Consumable
}