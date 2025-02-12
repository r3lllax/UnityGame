using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    public PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохранить объект между сценами
            LoadPlayerData(); // Загрузить данные при старте
        }
        else
        {
            Destroy(gameObject); // Удалить дубликат
        }
    }

    private void OnDestroy()
    {
        SavePlayerData(); // Сохранить данные при уничтожении объекта
    }

    public void SavePlayerData()
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
        Debug.Log("Player data saved to: " + Application.persistentDataPath);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Player data loaded.");
        }
        else
        {
            Debug.Log("No save file found. Creating new player data.");
            playerData = new PlayerData(); // Создать новые данные, если файла нет
        }
    }
}