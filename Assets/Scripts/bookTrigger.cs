using UnityEngine;

public class bookTrigger : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject DialogueObj;
    [SerializeField] private GameObject statesTint;
    private bool inTrigger, inDialogue, DialogueWas;
    void Awake()
    {
        inTrigger = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!DialogueWas){
            inTrigger = true;
            text.SetActive(true);
        }
    }

    void Update()
    {
        if(inTrigger && Input.GetKeyDown(KeyCode.E)){
            if(!inDialogue){
                DialogueObj.SetActive(true);
                DialogueObj.GetComponent<DialogueSystem>().StartDialogue();
                text.SetActive(false);
                inDialogue = true;
                DialogueWas = true;
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        text.SetActive(false);
        inTrigger = false;
    }
    
}
