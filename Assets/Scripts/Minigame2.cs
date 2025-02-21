using UnityEngine;

public class Minigame2 : MonoBehaviour
{
    public static int AttemptCount;
    [SerializeField] private GameObject Player;


    void Start()
    {
        AttemptCount = PlayerDataManager.Instance.playerData.health / 10;
        Debug.Log($"Количество попыток у игрока {AttemptCount}");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"тег колизии:{collision.name}");
        if(collision.name =="LavaTrigger"&& AttemptCount >0){
            Player.GetComponent<Transform>().position = new Vector2(-6.41f,1f);
            Minigame2.AttemptCount-=1;
        }
    }
}
