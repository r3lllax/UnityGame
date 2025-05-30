using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;

public class PanelController : MonoBehaviour
{
    
    public RectTransform panel;
    public ItemsList itemsList;
    public float slideSpeed = 5f;
    private bool isPanelVisible = true;
    private Vector2 targetPosition;
    public GameObject Trigger;
    public TextMeshProUGUI Text;
    public Image Image;
    public int targetX;
    public int targetX2;
    public int targetY;
    public int targetY2;

    private void Start()
    {
        // Начальная позиция панели (за пределами экрана)
        targetPosition = panel.anchoredPosition;
    }

    private void Update()
    {
        
        // Плавное перемещение панели
        panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, targetPosition, Time.deltaTime * slideSpeed);
    }

    public void TogglePanel()
    {
        isPanelVisible = !isPanelVisible;
        if (isPanelVisible)
        {
            // Позиция, когда панель видна
            targetPosition = new Vector2(targetX, targetY); // Пример позиции
        }
        else
        {
            // Позиция, когда панель скрыта
            targetPosition = new Vector2(targetX2, targetY2); // Пример позиции
        }
        Debug.Log("togglePanel");
    }

    public void showInventory(){
        Debug.Log("Инвентарь: ");
        foreach(var item in PlayerDataManager.Instance.playerData.inventory.items){
            Debug.Log(item.itemName);
            Debug.Log(item.Type);
            Debug.Log(item.Strength);
            Debug.Log(item.ImagePath);
        }
    }

    public void Accept(){
        if(itemsList.ItemType == "Damage"){
            PlayerDataManager.Instance.playerData.attackPower += itemsList.ItemStrength;
        }
        else{
            PlayerDataManager.Instance.playerData.health += itemsList.ItemStrength;
        }
        SaveLoadManager.Instance.inventory.AddItem(new Item(itemsList.ItemName,itemsList.ItemStrength,itemsList.ItemType,itemsList.ItemDescription,"Дебаф",itemsList.ItemPicture));
        SaveLoadManager.Instance.SaveInventory();
        PlayerDataManager.Instance.playerData.inventory = SaveLoadManager.Instance.inventory;
        PlayerDataManager.Instance.SavePlayerData();
        SaveLoadManager.Instance.inventory.PrintInventory();
        Debug.Log("Предмет добавлен, данные сохранены");
        Trigger.GetComponent<ChestTrigger>().text.SetActive(false);
        Text.text = "Вы забрали предмет";
        Trigger.GetComponent<ChestTrigger>().enabled = false;
        
    }

    public void LoadImage(string fileName, Image Image)
    {
        // Путь к изображению
        string path = Path.Combine(Application.dataPath, "Images", fileName);

        if (File.Exists(path))
        {
            // Загружаем данные изображения
            byte[] imageData = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);

            // Создаем спрайт из текстуры
            Sprite newSprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );

            // Назначаем спрайт компоненту Image
            Image.sprite = newSprite;
        }
        else
        {
            Debug.LogError("Изображение не найдено по пути: " + path);
        }
    }
    
}