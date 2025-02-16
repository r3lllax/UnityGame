using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
    private Animator animator;
    private bool isTrigger = false;
    private int player,enemy,playerWins, enemyWins;
    private int Count = 0;
    
    [SerializeField]private GameObject text;
    [SerializeField]private TextMeshProUGUI playerText;
    [SerializeField]private TextMeshProUGUI enemyText;
    [SerializeField]private GameObject[] ObgToOn;
    [SerializeField]private TextMeshProUGUI playerCurrentScore;
    [SerializeField]private TextMeshProUGUI EnemyCurrentScore;


    private System.Random random;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        random = new System.Random();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.SetActive(true);
        isTrigger = !isTrigger;
    }

    private IEnumerator WaitSec(){
        animator.SetBool("TurnOn",true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("TurnOn",false);
        
    }

    private void Update()
    {
        
        playerText.text = playerWins.ToString();
        enemyText.text = enemyWins.ToString();
        if(isTrigger && Input.GetKeyDown(KeyCode.E)){
            if(playerWins <3 && enemyWins <3){
                
                StartCoroutine(WaitSec());
                
                foreach(var obj in ObgToOn){
                    obj.SetActive(true);
                }
                if(playerCurrentScore!= null){
                    playerCurrentScore.color = new Color32(255,255,255,255);
                }
                if(EnemyCurrentScore!= null){
                    EnemyCurrentScore.color = new Color32(255,255,255,255);
                }
                Count++;
                player = random.Next(0,100);
                enemy = random.Next(0,100);
                Debug.Log($"Player - {player}, enemy - {enemy}");
                playerCurrentScore.text = $"Выпало: {player}";
                EnemyCurrentScore.text = $"Выпало: {enemy}";

                if(player > enemy){
                    Debug.Log($"В раунде номер {Count} победил игрок");
                    playerCurrentScore.color = new Color32(53, 189, 1, 255);
                    EnemyCurrentScore.color = new Color32(189, 53, 1, 255);
                    playerWins++;
                }
                else if(player<enemy){
                    Debug.Log($"В раунде номер {Count} победил противник");
                    EnemyCurrentScore.color = new Color32(53, 189, 1, 255);
                    playerCurrentScore.color = new Color32(189, 53, 1, 255);
                    enemyWins++;
                }
                else{
                    Debug.Log($"В раунде номер {Count} ничья");
                }
                
                if(playerWins == 3){
                    Debug.Log("Игрок выиграл, ура!");
                    animator.SetBool("TurnOn",false);
                    text.SetActive(false);
                    Destroy(GetComponent<ButtonTrigger>());
                    foreach(var obj in ObgToOn){
                        obj.SetActive(false);
                    }

                }
                else if(enemyWins == 3){
                    Debug.Log("Игрок проиграл, смерть");
                    animator.SetBool("TurnOn",false);
                    text.SetActive(false);
                    Destroy(GetComponent<ButtonTrigger>());
                    foreach(var obj in ObgToOn){
                        obj.SetActive(false);
                    }

                }

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        text.SetActive(false);
        isTrigger = !isTrigger;
    }
}
