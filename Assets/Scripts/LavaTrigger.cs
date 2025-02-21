using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class LavaTrigger : MonoBehaviour
{
    private int Dmg;
    private bool inLava = false;
    void Start()
    {
        Dmg = PlayerDataManager.Instance.playerData.health/4;
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
        if(Minigame2.AttemptCount == 0){
            PlayerDataManager.Instance.playerData.health -=Dmg;
        }
        yield return new WaitForSeconds(1);
        inLava = true;
    }
}
