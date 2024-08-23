using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    // Guarda el progreso del juego
    public void SaveGame()
    {
        GameManager.Instance.SaveGameData();
        Debug.Log("Progreso guardado.");
    }

    // Carga el progreso del juego
    public void LoadGame()
    {
        GameManager.Instance.LoadGameData();
        Debug.Log("Progreso cargado.");
    }
}