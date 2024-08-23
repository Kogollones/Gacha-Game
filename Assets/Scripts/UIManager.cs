using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text goldText;
    public Text gemsText;
    public Text stageText;
    public Button gachaButton;
    public Button upgradeButton;
    public Button nextStageButton;

    public GameObject upgradePanel;
    public GameObject characterInfoPanel;
    public GameObject inventoryPanel;

    public Text equippedWeaponNameText;
    public Image equippedWeaponIconImage;
    public Text equippedArmorNameText;
    public Image equippedArmorIconImage;

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
            return;
        }
    }

    void Start()
    {
        UpdateGoldDisplay(GameManager.Instance.gold);
        UpdateGemsDisplay(GameManager.Instance.gems);
        UpdateStageDisplay();

        gachaButton.onClick.AddListener(GameManager.Instance.gachaManager.PerformGachaPull);
        upgradeButton.onClick.AddListener(OpenUpgradePanel);
        nextStageButton.onClick.AddListener(GameManager.Instance.stageManager.StartNextStage);
    }

    public void UpdateGoldDisplay(int goldAmount)
    {
        goldText.text = "Oro: " + goldAmount;
    }

    public void UpdateGemsDisplay(int gemsAmount)
    {
        gemsText.text = "Gemas: " + gemsAmount;
    }

    public void UpdateStageDisplay()
    {
        stageText.text = "Escenario: " + (GameManager.Instance.stageManager.currentStage + 1);
    }

    public void UpdateInventoryDisplay()
    {
        // Implementa la lógica para mostrar los objetos del inventario en la UI
    }

    public void UpdateEquippedWeaponDisplay(Weapon weapon)
    {
        if (weapon != null)
        {
            equippedWeaponNameText.text = weapon.name;
            equippedWeaponIconImage.sprite = weapon.icon;
        }
        else
        {
            equippedWeaponNameText.text = "Sin arma equipada";
            equippedWeaponIconImage.sprite = null;
        }
    }

    public void UpdateEquippedArmorDisplay(Armor armor)
    {
        if (armor != null)
        {
            equippedArmorNameText.text = armor.name;
            equippedArmorIconImage.sprite = armor.icon;
        }
        else
        {
            equippedArmorNameText.text = "Sin armadura equipada";
            equippedArmorIconImage.sprite = null;
        }
    }

    public void UpdateCharacterStatsDisplay(PlayerStats character)
    {
        // Implementa la lógica para mostrar las estadísticas del personaje en la UI
    }

    public void UpdateSkillDisplay(PlayerStats character, int skillIndex = -1)
    {
        // Implementa la lógica para mostrar las habilidades del personaje en la UI
    }

    public void OpenUpgradePanel()
    {
        upgradePanel.SetActive(true);
    }

    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }

    public void OpenCharacterInfoPanel(PlayerStats character)
    {
        characterInfoPanel.SetActive(true);
        UpdateCharacterStatsDisplay(character);
    }

    public void CloseCharacterInfoPanel()
    {
        characterInfoPanel.SetActive(false);
    }

    public void OpenInventoryPanel()
    {
        inventoryPanel.SetActive(true);
        UpdateInventoryDisplay();
    }

    public void CloseInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }
}