using UnityEngine;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public int stagesPerMap = 30;
    public int currentStage = 0;
    public int currentMap = 0;

    public List<Stage> stages = new List<Stage>
    {
        // Mapa 1
        new Stage { stageNumber = 1,  enemyLevel = 1,  isBoss = false, experienceReward = 50,  goldReward = 10 },
        new Stage { stageNumber = 2,  enemyLevel = 2,  isBoss = false, experienceReward = 60,  goldReward = 12 },
        // ... otros stages regulares
        new Stage { stageNumber = 10, enemyLevel = 5,  isBoss = true,  experienceReward = 200, goldReward = 40 },
        // ... otros stages regulares
        new Stage { stageNumber = 20, enemyLevel = 10, isBoss = true,  experienceReward = 400, goldReward = 80 },
        // ... otros stages regulares
        new Stage { stageNumber = 30, enemyLevel = 15, isBoss = true,  experienceReward = 800, goldReward = 160 },

        // Mapa 2 (y así sucesivamente)
        // ...
    };

    public BattleManager battleManager;
    public GameObject[] enemyPrefabs; // Array de prefabs de enemigos (regular, jefe, jefe final)

    public delegate void BattleWonEvent();
    public event BattleWonEvent OnBattleWon;

    void Start()
    {
        if (enemyPrefabs.Length < 3)
        {
            Debug.LogError("Se necesitan al menos 3 prefabs de enemigos en StageManager (regular, jefe, jefe final).");
        }
    }

    public void StartNextStage()
    {
        currentStage++;

        if (currentStage <= stagesPerMap)
        {
            Stage nextStage = stages[currentStage - 1];

            GameObject enemyPrefab = GetEnemyPrefabForStage(nextStage);

            GameObject enemyObject = Instantiate(enemyPrefab);
            EnemyStats enemyStats = enemyObject.GetComponent<EnemyStats>();
            enemyStats.Initialize(nextStage.enemyLevel);

            battleManager.StartBattle(FindFirstObjectByType<PlayerStats>(), enemyStats);
        }
        else
        {
            currentMap++;
            currentStage = 0; 

            if (currentMap < GetTotalMaps())
            {
                Debug.Log($"¡Has completado el mapa {currentMap}! Desbloqueando el siguiente mapa.");
            }
            else
            {
                Debug.Log("¡Has completado todos los mapas! ¡Felicidades!");
            }
        }
    }

    public bool CanProgressToNextStage()
    {
        PlayerStats playerStats = FindFirstObjectByType<PlayerStats>();
        if (playerStats != null)
        {
            return playerStats.level >= stages[currentStage].enemyLevel; 
        }
        return false;
    }

    private int GetTotalMaps()
    {
        return Mathf.CeilToInt((float)stages.Count / stagesPerMap);
    }

    private GameObject GetEnemyPrefabForStage(Stage stage)
    {
        if (stage.isBoss)
        {
            if (stage.stageNumber == stagesPerMap)
            {
                return enemyPrefabs[2]; 
            }
            else
            {
                return enemyPrefabs[1]; 
            }
        }
        else
        {
            return enemyPrefabs[0]; 
        }
    }

    public void OnBattleWonHandler()
    {
        OnBattleWon?.Invoke();

        if (CanProgressToNextStage())
        {
            UIManager.Instance.UpdateStageDisplay();
            Invoke("StartNextStage", 1f);
        }
        else
        {
            Debug.Log("¡Necesitas ser más fuerte para avanzar al siguiente escenario!");
        }
    }
}

[System.Serializable]
public class Stage
{
    public int stageNumber;
    public int enemyLevel;   
    public bool isBoss;       
    public int experienceReward;
    public int goldReward;
}