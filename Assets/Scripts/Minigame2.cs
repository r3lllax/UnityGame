using System.Collections;
using UnityEngine;

public class Minigame2 : MonoBehaviour
{
    public static bool Minigame2ChestOpened = false;
    public static int AttemptCount;
    [SerializeField] private GameObject Player;


    void Start()
    {
        AttemptCount = PlayerDataManager.Instance.playerData.health / 10;
        Debug.Log($"Количество попыток у игрока {AttemptCount}");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log($"тег колизии:{collision.name}");
        if(collision.name =="LavaTrigger"&& AttemptCount >0){
            StartCoroutine(WhileAttemtAnim());
            Player.GetComponent<playerController>().ReserveAttempt();
            Minigame2.AttemptCount-=1;
            StartCoroutine(WhileAttemtAnim());
        }
        else if(collision.name == "LavaTrigger" && AttemptCount <=0){
            
            Player.GetComponent<SpriteRenderer>().color = new Color32(255,99,71,255);
        }
        else if(collision.name == "PlatformTrigger"){
            Player.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        
    }
    private IEnumerator WhileAttemtAnim(){
        GetComponent<CircleCollider2D>().enabled = false;
        Player.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<CircleCollider2D>().enabled = true;
        Player.GetComponent<CircleCollider2D>().enabled = true;
    }
    public void setAttempsZero(){
        AttemptCount = 0;
    }
}
