using UnityEngine;

public class bookTrigger : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject StateText;
    [SerializeField] private GameObject AttemptsText;
    [SerializeField] private GameObject DialogueObj;
    [SerializeField] private GameObject statesTint;
    private bool inTrigger, inDialogue, DialogueWas;
    public static bool ShowUI;
    void Awake()
    {
        inTrigger = false;
        ShowUI = DialogueSystem.Dialogue;
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
        if(ShowUI != false){
            ShowUI = DialogueSystem.Dialogue;
        }
        if(ShowUI == false){
            AttemptsText.SetActive(true);
            StateText.SetActive(true);
        }
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
