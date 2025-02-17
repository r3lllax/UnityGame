using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class DialogueWithMonster : MonoBehaviour
{
    [SerializeField] private GameObject StartDialogueText;
    [SerializeField] private GameObject DialogueObj;
    private bool innTrigger = false,inDialogue = false;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(DialogueSystem.NowIsADialogue == false){
            StartDialogueText.SetActive(true);
            innTrigger = !innTrigger;
        }
    }
    void Update()
    {
        if(DialogueSystem.Dialogue == false){
            Destroy(GetComponent<DialogueWithMonster>());
        }
        if (innTrigger && Input.GetKeyDown(KeyCode.E))
        {
            
            if(!inDialogue){
                DialogueObj.SetActive(true);
                DialogueObj.GetComponent<DialogueSystem>().StartDialogue();
                StartDialogueText.SetActive(false);
                inDialogue = true;
            }
            
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
      
        StartDialogueText.SetActive(false);
        innTrigger = !innTrigger;
    
    }
}
