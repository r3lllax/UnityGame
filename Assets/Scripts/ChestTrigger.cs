using UnityEditor.SearchService;
using UnityEngine;
using TMPro;

public class ChestTrigger : MonoBehaviour
{

    private bool playerInTrigger = false;
    public PanelController panelController; // Ссылка на PanelController
    public GameObject text;
    public GameObject Trigger;
    public TextMeshProUGUI Text;

    
    void Start()
    {
        if(PlayerDataManager.Instance.playerData.inventory.items.Count ==4){
            Text.text = "Ваш инвентарь полон";
            Trigger.GetComponent<ChestTrigger>().enabled = false;
        }

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
                panelController.TogglePanel();
                
                playerInTrigger = false;
                
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
