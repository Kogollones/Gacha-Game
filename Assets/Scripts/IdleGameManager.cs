using UnityEngine;

// Gestiona la generación pasiva de recursos cuando el juego está en segundo plano o cerrado
public class IdleGameManager : MonoBehaviour
{
    public int idleGoldPerMinute = 10;
    public int idleGemsPerHour = 1;

    private float idleTimer = 0f;

    public GameManager gameManager;

    void Start()
    {
        // Cargar el tiempo de la última pausa desde PlayerPrefs
        float lastPauseTime = PlayerPrefs.GetFloat("LastPauseTime", 0f);

        // Calcular y otorgar recursos idle si el juego estuvo pausado
        if (lastPauseTime > 0f)
        {
            float timePassed = Time.time - lastPauseTime;

            int goldEarned = Mathf.FloorToInt(timePassed / 60f) * idleGoldPerMinute;
            int gemsEarned = Mathf.FloorToInt(timePassed / 3600f) * idleGemsPerHour;

            gameManager.AddGold(goldEarned);
            gameManager.AddGems(gemsEarned);

            // Limpiar el tiempo de la última pausa
            PlayerPrefs.DeleteKey("LastPauseTime");
        }
    }
	
	void Update()
    {
        // Solo generar recursos si el juego NO está enfocado (en segundo plano)
        if (Application.isFocused == false) 
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= 60f)
            {
                GenerateIdleGold();
                idleTimer -= 60f; 
            }

            if (idleTimer >= 3600f) 
            {
                GenerateIdleGems();
                idleTimer -= 3600f; 
            }
        }
    }

    // Genera oro de forma pasiva
    void GenerateIdleGold()
    {
        gameManager.AddGold(idleGoldPerMinute);
    }

    // Genera gemas de forma pasiva
    void GenerateIdleGems()
    {
        gameManager.AddGems(idleGemsPerHour);
    }

    // Maneja la pausa y reanudación del juego, calculando los recursos generados mientras estaba en segundo plano
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefs.SetFloat("LastPauseTime", Time.time); 
        }
        else
        {
            float lastPauseTime = PlayerPrefs.GetFloat("LastPauseTime", 0f);
            float timePassed = Time.time - lastPauseTime;

            int goldEarned = Mathf.FloorToInt(timePassed / 60f) * idleGoldPerMinute;
            int gemsEarned = Mathf.FloorToInt(timePassed / 3600f) * idleGemsPerHour;

            gameManager.AddGold(goldEarned);
            gameManager.AddGems(gemsEarned);
        }
    }

    // Método llamado cuando el jugador gana una batalla (puedes añadir recompensas especiales aquí)
    public void OnBattleWon()
    {
        // Lógica para otorgar recompensas adicionales por victorias, etc.
    }
}