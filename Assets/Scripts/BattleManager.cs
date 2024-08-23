using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public PlayerStats player;
    public EnemyStats enemy;

    public void StartBattle(PlayerStats playerStats, EnemyStats enemyStats)
    {
        player = playerStats;
        enemy = enemyStats;

        // Iniciar la lógica de batalla
        PerformBattleRound();
    }

    private void PerformBattleRound()
    {
        // Lógica simplificada de batalla por turnos
        while (player.currentHealth > 0 && enemy.currentHealth > 0)
        {
            // Turno del jugador
            int playerDamage = player.CalculateDamage();
            enemy.TakeDamage(playerDamage);

            if (enemy.currentHealth <= 0)
            {
                EndBattle(true);
                return;
            }

            // Turno del enemigo
            int enemyDamage = enemy.CalculateDamage();
            player.TakeDamage(enemyDamage);

            if (player.currentHealth <= 0)
            {
                EndBattle(false);
                return;
            }
        }
    }

    private void EndBattle(bool playerWon)
    {
        if (playerWon)
        {
            // Recompensas por victoria
            int experienceReward = enemy.experienceReward;
            int goldReward = enemy.goldReward;

            player.GainExperience(experienceReward);
            player.AddGold(goldReward);

            Debug.Log($"¡Victoria! Ganaste {experienceReward} de experiencia y {goldReward} de oro.");

            GameManager.Instance.stageManager.OnBattleWonHandler();
        }
        else
        {
            Debug.Log("¡Derrota! Inténtalo de nuevo.");
            // Lógica para manejar la derrota (reiniciar nivel, etc.)
        }

        // Actualizar la UI
        UIManager.Instance.UpdateGoldDisplay(player.gold);
        UIManager.Instance.UpdateCharacterStatsDisplay(player);
    }
}