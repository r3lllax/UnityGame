using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.VFX;

public class pressButtons : MonoBehaviour
{
    public static int PlayerScore;
    public static int EnemeyScore;
    [SerializeField] private string Type;
    [SerializeField] private TextMeshProUGUI ScorePlayer;
    [SerializeField] private TextMeshProUGUI ScoreEnemy;
    public List<string> ButtonsNames = new List<string>();
    public List<GameObject> Buttons = new List<GameObject>();
    private int ButtonsCount = 4;
    [SerializeField] private GameObject PrefabBtn;
    private int currentIndexOfButton;
    private System.Random random = new System.Random();
    private float startPos;

    void Awake()
    {
        currentIndexOfButton = 0;
        PlayerScore =0;
        EnemeyScore =0;
    }
    void Start()
    {
        GenerateButtons();
        Debug.Log(Buttons.Count);
    }
    private void GenerateButtons(){
        startPos = -411f;
        for(int i = 0;i<ButtonsCount;i++){
            startPos+=160f;
            var btn = Instantiate(PrefabBtn).transform;
            btn.SetParent(transform, false);
            btn.localPosition = new Vector2(startPos,0);
            Buttons.Add(btn.gameObject);
        }
    }
    void Update()
    {
        var input = Input.inputString;
        if(Type != "Player"){
            ScorePlayer.text = PlayerScore.ToString();
            ScoreEnemy.text = EnemeyScore.ToString();
            if(random.Next(0,301)<300){
                return;
            }
            input = random.Next(0,6)>3?ButtonsNames[currentIndexOfButton].ToLower():"";
            
        }
        
        if(input.Length>=1){
            Debug.Log($"Инпут - {input}");
            Debug.Log($"Нужно - {ButtonsNames[currentIndexOfButton].ToLower()}");
            Debug.Log(input.Equals(ButtonsNames[currentIndexOfButton].ToLower()));
            if(input.Equals(ButtonsNames[currentIndexOfButton].ToLower())){
                currentIndexOfButton++;
                if(currentIndexOfButton == 4){
                    Debug.Log("Все кнопки нажаты");
                    if(Type == "Player"){
                        PlayerScore+=1;
                    }
                    else{
                        EnemeyScore+=1;
                    }
                    for(int i = 0;i<Buttons.Count;i++){
                        Destroy(Buttons[i]);
                    }
                    Buttons.Clear();
                    ButtonsNames.Clear();
                    GenerateButtons();
                    currentIndexOfButton = 0;
                }
            }
            else{
                currentIndexOfButton = 0;
            }
            Debug.Log("Осталось нажать:");
            for(int i = 0;i<Buttons.Count;i++){
                // Buttons[i].SetActive(false);
                Buttons[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(0,0,0,150);
            }
            for(int i = currentIndexOfButton;i<Buttons.Count;i++){
                Buttons[i].SetActive(true);
                Buttons[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);

                // Debug.Log(ButtonsNames[i]);
            }
            
            
        }
    }

}
