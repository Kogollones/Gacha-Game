using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public Weapon equippedWeapon;
    public Armor equippedArmor;

    public void AddItem(Item item)
    {
        items.Add(item);
        UIManager.Instance.UpdateInventoryDisplay();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        UIManager.Instance.UpdateInventoryDisplay();
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (equippedWeapon != null)
        {
            items.Add(equippedWeapon);
        }
        equippedWeapon = weapon;
        items.Remove(weapon);
        UIManager.Instance.UpdateEquippedWeaponDisplay(equippedWeapon);
    }

    public void EquipArmor(Armor armor)
    {
        if (equippedArmor != null)
        {
            items.Add(equippedArmor);
        }
        equippedArmor = armor;
        items.Remove(armor);
        UIManager.Instance.UpdateEquippedArmorDisplay(equippedArmor);
    }

    public void SaveInventory()
    {
        PlayerPrefs.SetInt("InventoryItemCount", items.Count);
        for (int i = 0; i < items.Count; i++)
        {
            PlayerPrefs.SetString($"InventoryItem_{i}_Name", items[i].name);
            PlayerPrefs.SetString($"InventoryItem_{i}_Type", items[i].itemType.ToString());
        }

        if (equippedWeapon != null)
        {
            PlayerPrefs.SetString("EquippedWeapon", equippedWeapon.name);
        }
        if (equippedArmor != null)
        {
            PlayerPrefs.SetString("EquippedArmor", equippedArmor.name);
        }
    }

    public void LoadInventory()
    {
        items.Clear();
        int itemCount = PlayerPrefs.GetInt("InventoryItemCount", 0);
        for (int i = 0; i < itemCount; i++)
        {
            string itemName = PlayerPrefs.GetString($"InventoryItem_{i}_Name", "");
            string itemTypeString = PlayerPrefs.GetString($"InventoryItem_{i}_Type", "");
            InventoryItemType itemType = (InventoryItemType)System.Enum.Parse(typeof(InventoryItemType), itemTypeString);

            Item newItem = CreateItemFromNameAndType(itemName, itemType);
            if (newItem != null)
            {
                items.Add(newItem);
            }
        }

        string equippedWeaponName = PlayerPrefs.GetString("EquippedWeapon", "");
        if (!string.IsNullOrEmpty(equippedWeaponName))
        {
            equippedWeapon = CreateItemFromNameAndType(equippedWeaponName, InventoryItemType.Weapon) as Weapon;
        }

        string equippedArmorName = PlayerPrefs.GetString("EquippedArmor", "");
        if (!string.IsNullOrEmpty(equippedArmorName))
        {
            equippedArmor = CreateItemFromNameAndType(equippedArmorName, InventoryItemType.Armor) as Armor;
        }

        UIManager.Instance.UpdateInventoryDisplay();
        UIManager.Instance.UpdateEquippedWeaponDisplay(equippedWeapon);
        UIManager.Instance.UpdateEquippedArmorDisplay(equippedArmor);
    }

    private Item CreateItemFromNameAndType(string itemName, InventoryItemType itemType)
    {
        // Aquí deberías implementar la lógica para crear el item correcto basado en su nombre y tipo
        // Por ejemplo, podrías tener un diccionario o una base de datos de items
        // Por ahora, solo crearemos objetos genéricos
        switch (itemType)
        {
            case InventoryItemType.Weapon:
                return new Weapon { name = itemName, itemType = itemType };
            case InventoryItemType.Armor:
                return new Armor { name = itemName, itemType = itemType };
            case InventoryItemType.Consumable:
                return new Consumable { name = itemName, itemType = itemType };
            default:
                return null;
        }
    }
}

public enum InventoryItemType
{
    Weapon,
    Armor,
    Consumable,
    Character
}

[System.Serializable]
public class Item
{
    public string name;
    public InventoryItemType itemType;
}

[System.Serializable]
public class Weapon : Item
{
    public int attackBonus;
}

[System.Serializable]
public class Armor : Item
{
    public int defenseBonus;
}

[System.Serializable]
public class Consumable : Item
{
    public int healAmount;
}