using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class DrawInventory : MonoBehaviour
{
    public GameObject myPrefab; // Префаб для отрисовки
    public Transform StartObject; // Начальная позиция
    public Transform parent; // Родительский объект для префабов

    private List<GameObject> createdItems = new List<GameObject>(); // Список для хранения созданных объектов

    public void Draw()
    {
        ClearInventory(); // Очищаем старые объекты перед созданием новых

        int offset = 0; // Начинаем с 0
        for (int i = 0; i < PlayerDataManager.Instance.playerData.inventory.items.Count; i++)
        {
            var CurrentItem = PlayerDataManager.Instance.playerData.inventory.items[i];
            GameObject newItem = DrawItem(StartObject.position.x, StartObject.position.y - offset, CurrentItem.itemName, CurrentItem.Type, CurrentItem.ImagePath, CurrentItem.Strength.ToString());
            createdItems.Add(newItem); // Добавляем созданный объект в список
            offset += 180; // Увеличиваем offset для следующего элемента
        }
    }

    public GameObject DrawItem(float StartX, float StartY, string ItemName, string ItemType, string ItemImage, string ItemStrength)
    {
        // Создаем экземпляр префаба
        GameObject item = Instantiate(myPrefab, new Vector2(StartX, StartY), Quaternion.identity, parent);

        // Получаем дочерние объекты
        Transform background = item.transform.GetChild(0); // Предположим, что background — это первый дочерний объект
        Transform nameTransform = background.transform.GetChild(0); // Name
        Transform imageTransform = background.transform.GetChild(1); // Image
        Transform typeTransform = background.transform.GetChild(2); // Type
        Transform strengthTransform = background.transform.GetChild(3); // Strength

        // Изменяем текстовые поля
        TextMeshProUGUI nameText = nameTransform.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI typeText = typeTransform.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI strengthText = strengthTransform.GetComponent<TextMeshProUGUI>();

        if (nameText != null)
        {
            nameText.text = ItemName;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI не найден на объекте Name!");
        }

        if (typeText != null)
        {
            typeText.text = ItemType=="Damage"?"Тип: Урон":"Тип: Здоровье";
        }
        else
        {
            Debug.LogError("TextMeshProUGUI не найден на объекте Type!");
        }

        if (strengthText != null)
        {
            strengthText.text = "Сила: "+ItemStrength;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI не найден на объекте Strength!");
        }

        // Получаем компонент Image
        Image imageComponent = imageTransform.GetComponent<Image>();
        if (imageComponent != null)
        {
            // Загружаем изображение и назначаем его компоненту Image
            LoadImage(ItemImage, imageComponent);
        }
        else
        {
            Debug.LogError("Image не найден на объекте Image!");
        }

        return item; // Возвращаем созданный объект
    }

    public void LoadImage(string fileName, Image imageComponent)
    {
        // Путь к изображению
        string path = Path.Combine(Application.dataPath, "Images", fileName);
        Debug.Log($"Путь к папке {path}");

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
            imageComponent.sprite = newSprite;
        }
        else
        {
            Debug.LogError("Изображение не найдено по пути: " + path);
        }
    }

    public void ClearInventory()
    {
        // Удаляем все объекты из списка
        foreach (GameObject item in createdItems)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }
        createdItems.Clear(); // Очищаем список
    }
}