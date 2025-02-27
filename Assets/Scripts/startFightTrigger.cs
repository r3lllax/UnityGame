using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class startFightTrigger : MonoBehaviour
{

    [SerializeField] private int minHP = 10;    
    [SerializeField] private int maxHP = 100; 
    [SerializeField] private float minTime = 30f;
    [SerializeField] private float maxTime = 60f;
    private double timer;
    [SerializeField] private GameObject TimerText;
    [SerializeField] private GameObject[] ButtonsToPress;

    public float CalculateTimer(int playerHP)
    {
        int clampedHP = Mathf.Clamp(playerHP, minHP, maxHP);
        float t = (float)(clampedHP - minHP) / (maxHP - minHP);
        float timer = Mathf.Lerp(maxTime, minTime, t);
        return timer;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player"){
            timer = Math.Round(CalculateTimer(PlayerDataManager.Instance.playerData.health),0);
            Debug.Log(Convert.ToInt32(timer));
            playerController.isFreezed = true;
            collision.GetComponent<Animator>().SetFloat("horizontalMove",0);
            collision.GetComponent<Animator>().SetFloat("verticalMove",0);
            GetComponent<PanelController>().TogglePanel();
            StartCoroutine(Timer());
            foreach(var obj in ButtonsToPress){
                obj.SetActive(true);
            }
        }
    }

    private IEnumerator Timer(){
        int TimerMax = Convert.ToInt32(timer);
        for(int i =0;i<TimerMax;i++){
            timer-=1;
            TimerText.GetComponent<TextMeshProUGUI>().text =timer.ToString();
            if(timer ==0){
                foreach(var obj in ButtonsToPress){
                    Destroy(obj);
                }
                if(pressButtons.EnemeyScore > pressButtons.PlayerScore){
                    Debug.Log("Противник победил");
                }
                else if(pressButtons.PlayerScore > pressButtons.EnemeyScore){
                    Debug.Log("Игрок победил");
                }
                else{
                    Debug.Log("Ничья");
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}

