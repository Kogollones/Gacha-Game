using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    public List<PlayerStats> unlockedCharacters = new List<PlayerStats>();
    public GameObject characterPrefab;
    public Transform characterSpawnPoint;

    public void UnlockCharacter(string characterId)
    {
        if (!IsCharacterUnlocked(characterId))
        {
            GameObject newCharacterObject = Instantiate(characterPrefab, characterSpawnPoint.position, Quaternion.identity);
            PlayerStats newCharacterStats = newCharacterObject.GetComponent<PlayerStats>();
            newCharacterStats.characterId = characterId;
            // Configura las estadísticas iniciales del personaje aquí
            unlockedCharacters.Add(newCharacterStats);
        }
        else
        {
            // El personaje ya está desbloqueado, aumenta su nivel de potencial
            PlayerStats existingCharacter = GetCharacter(characterId);
            existingCharacter.potentialLevel++;
        }
    }

    public bool IsCharacterUnlocked(string characterId)
    {
        return unlockedCharacters.Exists(c => c.characterId == characterId);
    }

    public PlayerStats GetCharacter(string characterId)
    {
        return unlockedCharacters.Find(c => c.characterId == characterId);
    }

    public int GetCharacterCount(string characterId)
    {
        return unlockedCharacters.Count(c => c.characterId == characterId);
    }

    public void SaveUnlockedCharacters()
    {
        PlayerPrefs.SetInt("UnlockedCharacterCount", unlockedCharacters.Count);
        for (int i = 0; i < unlockedCharacters.Count; i++)
        {
            PlayerStats character = unlockedCharacters[i];
            PlayerPrefs.SetString($"Character_{i}_Id", character.characterId);
            PlayerPrefs.SetString($"Character_{i}_Name", character.characterName);
            PlayerPrefs.SetInt($"Character_{i}_MaxHealth", character.maxHealth);
            PlayerPrefs.SetInt($"Character_{i}_AttackPower", character.attackPower);
            PlayerPrefs.SetInt($"Character_{i}_Defense", character.defense);
            PlayerPrefs.SetInt($"Character_{i}_CritChance", character.critChance);
            PlayerPrefs.SetFloat($"Character_{i}_CritDamage", character.critDamage);
            PlayerPrefs.SetInt($"Character_{i}_Level", character.level);
            PlayerPrefs.SetInt($"Character_{i}_Experience", character.experience);
            PlayerPrefs.SetInt($"Character_{i}_PotentialLevel", character.potentialLevel);
        }
        PlayerPrefs.Save();
    }

    public void LoadUnlockedCharacters()
    {
        int count = PlayerPrefs.GetInt("UnlockedCharacterCount", 0);
        for (int i = 0; i < count; i++)
        {
            string characterId = PlayerPrefs.GetString($"Character_{i}_Id", "");
            string characterName = PlayerPrefs.GetString($"Character_{i}_Name", "");
            int maxHealth = PlayerPrefs.GetInt($"Character_{i}_MaxHealth", 0);
            int attackPower = PlayerPrefs.GetInt($"Character_{i}_AttackPower", 0);
            int defense = PlayerPrefs.GetInt($"Character_{i}_Defense", 0);
            int critChance = PlayerPrefs.GetInt($"Character_{i}_CritChance", 0);
            float critDamage = PlayerPrefs.GetFloat($"Character_{i}_CritDamage", 0f);
            int level = PlayerPrefs.GetInt($"Character_{i}_Level", 1);
            int experience = PlayerPrefs.GetInt($"Character_{i}_Experience", 0);
            int potentialLevel = PlayerPrefs.GetInt($"Character_{i}_PotentialLevel", 0);

            GameObject newCharacterObject = Instantiate(characterPrefab, characterSpawnPoint.position, Quaternion.identity);
            PlayerStats newCharacterStats = newCharacterObject.GetComponent<PlayerStats>();
            newCharacterStats.characterId = characterId;
            newCharacterStats.characterName = characterName;
            newCharacterStats.maxHealth = maxHealth;
            newCharacterStats.attackPower = attackPower;
            newCharacterStats.defense = defense;
            newCharacterStats.critChance = critChance;
            newCharacterStats.critDamage = critDamage;
            newCharacterStats.level = level;
            newCharacterStats.experience = experience;
            newCharacterStats.potentialLevel = potentialLevel;

            unlockedCharacters.Add(newCharacterStats);
        }
    }
}