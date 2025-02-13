using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StrengthController1 : MonoBehaviour
{
    public TextMeshProUGUI textElement; // Ссылка на компонент Text

    private void Start()
    {
        // Проверка, что текстовый элемент назначен
        if (textElement == null)
        {
            Debug.LogError("Text element is not assigned!");
            return;
        }

        // Вызов метода UpdateText каждую секунду, начиная с 1 секунды
        InvokeRepeating("UpdateText", 1f, 1f);
    }

    private void UpdateText()
    {
        
        textElement.text = "Ваша сила атаки:" + PlayerDataManager.Instance.playerData.attackPower; 
    }
}
