using Unity.VisualScripting;
using UnityEngine;

public class TableTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject fightTrigger;
    public static bool isTrigger, dialogueWas;
    void Awake()
    {
        dialogueWas = false;
        isTrigger=false;
        DialogueSystem.Dialogue = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(DialogueSystem.Dialogue);
        if (DialogueSystem.Dialogue == false && dialogueWas == false) 
        {
            text.SetActive(true);
            isTrigger = true;
            
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        isTrigger = true;
    }

    void Update()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.E))
        {
                if (DialogueSystem.Dialogue == false && dialogueWas == false)
                {
                    text.SetActive(false);
                    dialogue.SetActive(true);
                    dialogueWas = true;
                }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        text.SetActive(false);
        isTrigger = false;
        fightTrigger.SetActive(true);
    }
}
