using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }

    public Inventory inventory;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохранить объект между сценами
            LoadInventory(); // Загрузить инвентарь при старте
        }
        else
        {
            Destroy(gameObject); // Удалить дубликат
        }
    }

    void OnDestroy()
    {
        SaveInventory(); // Сохранить инвентарь при уничтожении объекта
    }

    public void SaveInventory()
    {
        string json = JsonUtility.ToJson(inventory);
        File.WriteAllText(Application.persistentDataPath + "/inventory.json", json);
        Debug.Log("Inventory saved to: " + Application.persistentDataPath);
    }

    public void LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            inventory = JsonUtility.FromJson<Inventory>(json);
            Debug.Log("Inventory loaded.");
        }
        else
        {
            Debug.Log("No save file found. Creating new inventory.");
            inventory = new Inventory(); // Создать новый инвентарь, если файла нет
        }
    }
}