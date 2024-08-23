using UnityEngine;

// Controla el comportamiento básico de un enemigo en un juego idle
public class EnemyController : MonoBehaviour
{
    public EnemyStats stats;

    void Start()
    {
        stats = GetComponent<EnemyStats>();
    }

    // El enemigo recibe daño
    public void TakeDamage(int damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
        {
            Die();
        }
    }

    // Maneja la muerte del enemigo
    void Die()
    {
        // Lógica para otorgar recompensas al jugador, destruir el objeto, etc.
        // ...

        // Notificar al BattleManager que el enemigo ha muerto
        BattleManager.Instance.OnEnemyDefeated(this); // Asegúrate de tener una instancia de BattleManager accesible

        Destroy(gameObject);
    }
}