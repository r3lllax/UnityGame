using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private bool playerInTrigger = false;
    public GameObject text;
    public int doorNum;
    public int EventType;
    public int miniGameType;

    private System.Random random;

    private void Start()
    {
        random = new System.Random();

        EventType = random.Next(0, 2); 
        miniGameType = EventType == 0 ? 3 : random.Next(4, 9);

        Debug.Log($"Объект: {gameObject.name}, EventType: {EventType}, miniGameType: {miniGameType}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "player")
        {
            Debug.Log("label for player active!");
            text.SetActive(true);
            playerInTrigger = true;
        }
    }

    void Update()
    {
        if (playerInTrigger == true)
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(PlayerDataManager.Instance.playerData.AvalibleDoors.Contains(doorNum)){
                    Debug.Log("Вход");
                    Debug.Log($"Тип ивента:{EventType}, номер мини-игры:{miniGameType}");
                    playerInTrigger = false;
                    PlayerDataManager.Instance.playerData.AvalibleDoors.Remove(doorNum);
                    PlayerDataManager.Instance.SavePlayerData();
                    gameObject.SetActive(false);

                    //СЮДА ДОБАВЛЯТЬ ПЕРЕХОД НА СЦЕНУ

                    Debug.Log("Оставшиеся двери");
                    for(int i =0 ;i<PlayerDataManager.Instance.playerData.AvalibleDoors.Count;i++){
                        Debug.Log(PlayerDataManager.Instance.playerData.AvalibleDoors[i]);
                    }
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "player")
        {
            Debug.Log("label for player disabled!");
            text.SetActive(false);
            playerInTrigger = false;
        }
    }
}