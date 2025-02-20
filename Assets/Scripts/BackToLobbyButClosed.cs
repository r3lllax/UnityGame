using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLobbyButClosed : MonoBehaviour
{
    public bool playerInTrigger;
    public GameObject text;

    public static bool DoorIsOpen = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            text.SetActive(true);
            if(DoorIsOpen){
                text.GetComponent<TextMeshProUGUI>().text = "Нажмите Е чтобы вернуться";
            }
            playerInTrigger = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            text.SetActive(false);
            playerInTrigger = false;
        }
    }

    void Update()
    {
        if(playerInTrigger){
            if (DoorIsOpen && Input.GetKeyDown(KeyCode.E))
            {
                                
                playerInTrigger = false;
                //Добавлять ondestroy чтобы сохранять при закрытии
                SaveLoadManager.Instance.SaveInventory();
                PlayerDataManager.Instance.playerData.inventory = SaveLoadManager.Instance.inventory;
                PlayerDataManager.Instance.SavePlayerData();
                //СЮДА ДОБАВЛЯТЬ ПЕРЕХОД НА СЦЕНУ
                DoorIsOpen = false;
                SceneManager.LoadScene(2);
                
            }
        }
    }
}
