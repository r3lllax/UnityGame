using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static bool Dialogue = true;
    public static bool NowIsADialogue = false;
    [SerializeField]private string[] lines;
    [SerializeField]private float speedText;
    [SerializeField]private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject text;
    private int index;

    private void Awake()
    {
        dialogueText.text = string.Empty;
    }
    void Start()
    {
        
    }
    public void StartDialogue(){
        index = 0;
        NowIsADialogue = true;
        StartCoroutine(PrintLines());
        
    }
    private IEnumerator PrintLines(){
        
        foreach(char c in lines[index].ToCharArray()){
            
            dialogueText.text+=c;
            Debug.Log(dialogueText.text);
            yield return new WaitForSeconds(speedText);
        }
    }
    private void NextLines(){
        if(index<lines.Length-1){
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(PrintLines());
        }
        else{
             gameObject.SetActive(false);
             Dialogue = false;
            //  ButtonTrigger.isTrigger = true;
             if(text){
                text.SetActive(true);
             }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            Skip();
        }
    }
    public void Skip(){
        if(dialogueText.text == lines[index]){
            NextLines();
        }
        else{
            StopAllCoroutines();
            dialogueText.text = lines[index];
        }
    }
}
