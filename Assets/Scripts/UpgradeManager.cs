using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public CharacterManager characterManager;

    public void UpgradeCharacter(PlayerStats character, string statToUpgrade)
    {
        int upgradeCost = CalculateUpgradeCost(character, statToUpgrade);

        if (GameManager.Instance.gold >= upgradeCost)
        {
            GameManager.Instance.gold -= upgradeCost;

            switch (statToUpgrade)
            {
                case "health":
                    character.maxHealth += 10;
                    break;
                case "attack":
                    character.attackPower += 5;
                    break;
                case "defense":
                    character.defense += 2;
                    break;
                case "critChance":
                    character.critChance += 1;
                    break;
                case "critDamage":
                    character.critDamage += 0.05f;
                    break;
            }

            UIManager.Instance.UpdateCharacterStatsDisplay(character);
            UIManager.Instance.UpdateGoldDisplay(GameManager.Instance.gold);
        }
        else
        {
            Debug.Log("No tienes suficiente oro para esta mejora.");
        }
    }

    public void UnlockPotential(PlayerStats character)
    {
        int requiredDuplicates = character.potentialLevel + 1;
        int characterCount = characterManager.GetCharacterCount(character.characterId);

        if (characterCount >= requiredDuplicates)
        {
            character.potentialLevel++;
            
            // Aumenta las estadísticas base del personaje
            character.maxHealth += 50;
            character.attackPower += 10;
            character.defense += 5;
            character.critChance += 2;
            character.critDamage += 0.1f;

            // Elimina los duplicados usados
            for (int i = 0; i < requiredDuplicates; i++)
            {
                characterManager.unlockedCharacters.Remove(characterManager.GetCharacter(character.characterId));
            }

            UIManager.Instance.UpdateCharacterStatsDisplay(character);
        }
        else
        {
            Debug.Log("No tienes suficientes duplicados para desbloquear el potencial.");
        }
    }

    private int CalculateUpgradeCost(PlayerStats character, string statToUpgrade)
    {
        int baseCost = 100;
        int levelMultiplier = character.level;

        switch (statToUpgrade)
        {
            case "health":
                return baseCost * levelMultiplier * 2;
            case "attack":
                return baseCost * levelMultiplier * 3;
            case "defense":
                return baseCost * levelMultiplier * 2;
            case "critChance":
                return baseCost * levelMultiplier * 4;
            case "critDamage":
                return baseCost * levelMultiplier * 5;
            default:
                return baseCost * levelMultiplier;
        }
    }
}