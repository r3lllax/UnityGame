using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class ItemsList : MonoBehaviour
{
    public static string[] LootImages = { "StoneOfStrength.png",
            "RuneOfSpeed.png",
            "Elixir.png",
            "headphones.png",
            "Bloody-knife.png",
            "water.png",
            "dumbbell.png",
            "protein.png",
            "Bloody Mask.png",
    };
    public static string[] Loot = { 
            "Камень силы",
            "Руна скорости",
                "Элексир","Наушники","Проклятый нож",
                "Вода","Гантель","Протеин",
            "Охотничья маска",};
    public static string[] LootCaption = {
            "Ускоряет вашу кровь",
            "Дает вам сверхчеловеческую скорость",
            "Вы чувствуете как ваши мышци наполняются силой",
            "Мотивирующая музыка придает вам сил",
            "Теперь вы можете резать врагов, не только продукты",
            "Вы утоляете жажду",
            "Теперь вы можете накачаться",
            "Вы выпиваете это и становитесь халком",
            "При прикасновении вас озоряет"};

    public string ItemName;
    public string ItemType;
    public int ItemStrength;
    public string ItemDescription;
    public string ItemPicture;
    private System.Random random;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Type; 
    public TextMeshProUGUI Strength;  
    public TextMeshProUGUI Description; 
    public Image Image;

    void Start()
    {
        random = new System.Random();
        int currentIndex = random.Next(0,Loot.Length);
        int currentType = random.Next(0,2);
        ItemName = Loot[currentIndex];
        ItemType = currentType==0?"Heal":"Damage";
        ItemDescription = LootCaption[currentIndex];
        ItemStrength = random.Next(0,100);
        ItemPicture = LootImages[currentIndex];

        Title.text = ItemName;
        Type.text = "Тип:"+ItemType;
        Strength.text = "Количество очков:"+ItemStrength.ToString();
        Description.text = ItemDescription;
        LoadImage(ItemPicture,Image);
        

        Debug.Log(ItemName);
        Debug.Log(ItemType);
        Debug.Log(ItemStrength);
        Debug.Log(ItemDescription);
        Debug.Log(ItemPicture);
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
