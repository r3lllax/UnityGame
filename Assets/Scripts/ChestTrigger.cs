using UnityEditor.SearchService;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{

    private bool playerInTrigger = false;
    public PanelController panelController; // Ссылка на PanelController
    public GameObject text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
