using UnityEngine;
using System.Collections.Generic;

public class GachaManager : MonoBehaviour
{
    public CharacterManager characterManager;
    public GachaPool gachaPool;

    public void PerformGachaPull()
    {
        if (GameManager.Instance.gems >= gachaPool.costPerPull)
        {
            GameManager.Instance.gems -= gachaPool.costPerPull;
            GachaItem pullResult = gachaPool.PullGacha();

            if (pullResult.itemType == InventoryItemType.Character)
            {
                characterManager.UnlockCharacter(pullResult.name);
            }
            else
            {
                // Añadir el item al inventario del jugador
                GameManager.Instance.inventory.AddItem(pullResult);
            }

            UIManager.Instance.UpdateGemsDisplay(GameManager.Instance.gems);
            // Actualizar la UI para mostrar el resultado del gacha
        }
        else
        {
            Debug.Log("No tienes suficientes gemas para realizar un pull.");
        }
    }
}