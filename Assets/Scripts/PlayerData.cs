using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health; // Здоровье игрока
    public int attackPower; // Сила атаки
    public Inventory inventory; // Инвентарь
    public List<int> AvalibleDoors; // Дополнительный список данных

    
}