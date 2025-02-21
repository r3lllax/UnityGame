using TMPro;
using UnityEngine;

public class AttemptController : MonoBehaviour
{
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = $"Количество ваших резервных попыток: {Minigame2.AttemptCount}";
    }
}
