using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    public List<Skill> skills = new List<Skill>
    {
        new Skill("Ataque Básico", 20, 0f, 0, DamageType.Physical), 
        new Skill("Golpe Poderoso", 40, 3f, 0, DamageType.Physical),    
        new Skill("Curación", -30, 5f, 0),         
        new Skill("Escudo", 0, 10f, 10f, DamageType.None),        
        new Skill("Bola de Fuego", 30, 2f, 0, DamageType.Magical),    
        new Skill("Rayo Congelante", 25, 4f, 5f, DamageType.Magical)   
    };

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    // Usa una habilidad si está disponible y el jugador tiene suficientes recursos
    public void UseSkill(int skillIndex)
    {
        if (skillIndex >= 0 && skillIndex < skills.Count)
        {
            Skill skill = skills[skillIndex];
            if (!skill.IsOnCooldown && CanAffordSkill(skill)) 
            {
                skill.UseSkill(playerStats); 
                skill.StartCooldown(); 
                PaySkillCost(skill);    
            }
            else if (!skill.IsOnCooldown)
            {
                Debug.Log("No tienes suficientes recursos para usar esta habilidad.");
            }
        }
    }

    // Desbloquea nuevas habilidades al subir de nivel
    public void UnlockNewSkills(int currentLevel)
    {
        if (currentLevel == 5 && !skills.Contains(new Skill("Golpe Poderoso", 40, 3f, 0, DamageType.Physical)))
        {
            skills.Add(new Skill("Golpe Poderoso", 40, 3f, 0, DamageType.Physical));
        }
        // ... desbloquear otras habilidades según el nivel
    }

    // Comprueba si el jugador puede permitirse usar la habilidad (coste de maná/energía)
    bool CanAffordSkill(Skill skill)
    {
        // Lógica para verificar si el jugador tiene suficientes recursos
        // ... (puedes implementar un sistema de maná o energía aquí)
        return true; // Por ahora, asumimos que todas las habilidades se pueden usar
    }

    // Resta los recursos necesarios para usar la habilidad
    void PaySkillCost(Skill skill)
    {
        // Lógica para restar maná o energía
        // ...
    }

    // Actualiza los tiempos de reutilización y efectos de todas las habilidades
    void Update()
    {
        float deltaTime = Time.deltaTime;
        foreach (Skill skill in skills)
        {
            skill.UpdateCooldown(deltaTime);
        }
    }
}

[System.Serializable]
public class Skill
{
    public string name;        
    public int damage;        
    public float cooldown;      
    public float effectDuration; 
    public DamageType damageType;
    private float cooldownTimer; 
    private float effectTimer;    

    public bool IsOnCooldown => cooldownTimer > 0f;
    public bool IsEffectActive => effectTimer > 0f;

    public Skill(string name, int damage, float cooldown, float effectDuration = 0f, DamageType damageType = DamageType.Physical) 
    {
        this.name = name;
        this.damage = damage;
        this.cooldown = cooldown;
        this.effectDuration = effectDuration;
        this.damageType = damageType;
        this.cooldownTimer = 0f;
        this.effectTimer = 0f;
    }

    // Usa la habilidad, aplicando su efecto al jugador
    public void UseSkill(PlayerStats playerStats)
    {
        Debug.Log($"Usando habilidad: {name}");

        if (damage > 0) 
        {
            // Lógica para infligir daño al enemigo (puedes usar BattleManager o acceder directamente)
            EnemyController enemy = FindObjectOfType<EnemyController>(); 
            if (enemy != null)
            {
                // Aplicar posibles efectos de estado o reducción de daño según el tipo de daño
                int finalDamage = CalculateDamageAfterEffects(damage, enemy.stats); 
                enemy.TakeDamage(finalDamage);
            }
        }
        else if (damage < 0) 
        {
            // Lógica para curar al jugador
            playerStats.currentHealth = Mathf.Min(playerStats.maxHealth, playerStats.currentHealth - Mathf.RoundToInt(damage)); 
        }

        if (effectDuration > 0f)
        {
            effectTimer = effectDuration;
        }
    }

    // Calcula el daño final después de aplicar posibles efectos o resistencias
    private int CalculateDamageAfterEffects(int baseDamage, EnemyStats enemyStats)
    {
        // Ejemplo: Reducir el daño mágico si el enemigo tiene resistencia mágica
        if (damageType == DamageType.Magical && enemyStats.magicResistance > 0)
        {
            float damageReduction = enemyStats.magicResistance / 100f;
            return Mathf.RoundToInt(baseDamage * (1 - damageReduction));
        }
        return baseDamage;
    }

    public void StartCooldown()
    {
        cooldownTimer = cooldown;
    }

    public void UpdateCooldown(float deltaTime)
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= deltaTime;
        }

        if (effectTimer > 0f)
        {
            effectTimer -= deltaTime;

            // Aplicar el efecto mientras esté activo
            if (name == "Escudo")
            {
                // Reducir el daño recibido en un 50%
                playerStats.defense += Mathf.RoundToInt(playerStats.defense * 0.5f); 
            }
            // ... otros efectos

            if (effectTimer <= 0f)
            {
                // Finalizar el efecto
                if (name == "Escudo")
                {
                    playerStats.defense -= Mathf.RoundToInt(playerStats.defense * 0.5f); 
                }
                // ... otros efectos
            }
        }
    }
}

public enum DamageType
{
    Physical,
    Magical,
    None 
}