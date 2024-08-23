using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public string enemyName;
    public int maxHealth;
    public int currentHealth;
    public int attackPower;
    public int defense;
    public int experienceReward;
    public int goldReward;

    public void Initialize(int level)
    {
        // Ajusta las estadísticas según el nivel del enemigo
        maxHealth = 50 + (level * 10);
        currentHealth = maxHealth;
        attackPower = 5 + (level * 2);
        defense = 2 + level;
        experienceReward = 20 + (level * 5);
        goldReward = 10 + (level * 2);
    }

    public void TakeDamage(int damage)
    {
        int damageAfterDefense = Mathf.Max(0, damage - defense);
        currentHealth -= damageAfterDefense;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int CalculateDamage()
    {
        return attackPower;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        // Puedes añadir más lógica aquí si es necesario
    }
}