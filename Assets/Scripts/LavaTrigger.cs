using System.Collections;
using UnityEngine;

public class LavaTrigger : MonoBehaviour
{
    private int Dmg;
    private bool inLava = false;
    void Start()
    {
        Dmg = PlayerDataManager.Instance.playerData.health/2;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player"){
            inLava = true;
            
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "player" && inLava){
            StartCoroutine(DamageOnLavaStay());
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
    }

    private IEnumerator DamageOnLavaStay(){
        inLava = false;
        Debug.Log($"Игрок стоит в лаве, нанесено {Dmg} урона");
        PlayerDataManager.Instance.playerData.health -=Dmg;
        yield return new WaitForSeconds(1);
        inLava = true;
    }
}
