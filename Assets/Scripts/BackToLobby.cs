using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLobby : MonoBehaviour
{   

    public bool playerInTrigger;
    public GameObject text;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            text.SetActive(true);
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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInTrigger){
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                Debug.Log("Выход");
                
                playerInTrigger = false;
                //Добавлять ondestroy чтобы сохранять при закрытии
                SaveLoadManager.Instance.SaveInventory();
                PlayerDataManager.Instance.playerData.inventory = SaveLoadManager.Instance.inventory;
                PlayerDataManager.Instance.SavePlayerData();
                //СЮДА ДОБАВЛЯТЬ ПЕРЕХОД НА СЦЕНУ

                SceneManager.LoadScene(2);
                
            }
        }
    }
}
