using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int gold;
    public int gems;

    public CharacterManager characterManager;
    public StageManager stageManager;
    public GachaManager gachaManager;
    public UpgradeManager upgradeManager;
    public BattleManager battleManager;
    public Inventory inventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("Gems", gems);
        PlayerPrefs.SetInt("CurrentStage", stageManager.currentStage);
        PlayerPrefs.SetInt("CurrentMap", stageManager.currentMap);

        characterManager.SaveUnlockedCharacters();
        inventory.SaveInventory();

        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        gold = PlayerPrefs.GetInt("Gold", 0);
        gems = PlayerPrefs.GetInt("Gems", 0);
        stageManager.currentStage = PlayerPrefs.GetInt("CurrentStage", 0);
        stageManager.currentMap = PlayerPrefs.GetInt("CurrentMap", 0);

        characterManager.LoadUnlockedCharacters();
        inventory.LoadInventory();

        UIManager.Instance.UpdateGoldDisplay(gold);
        UIManager.Instance.UpdateGemsDisplay(gems);
        UIManager.Instance.UpdateStageDisplay();
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    // Puedes añadir más métodos aquí para manejar la lógica general del juego
}