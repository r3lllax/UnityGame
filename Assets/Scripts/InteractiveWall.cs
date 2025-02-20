using System;
using UnityEngine;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;

public class InteractiveWall : MonoBehaviour
{
    [SerializeField] private GameObject Visual;

    [SerializeField] private BoxCollider2D Collider;
    private string CurrentState;
    private System.Random random = new System.Random();
    public static String[] States = {
        "Fire",
        "Water",
        "Wind"
    };
    public  Sprite[] StatesImages = {};
    
    void Start()
    {
        int index = random.Next(0,States.Length);
        CurrentState = States[index];
        Visual.GetComponent<SpriteRenderer>().sprite = StatesImages[index];
        Debug.Log(StatesImages[index]);
        Debug.Log($"Стойка: {CurrentState}");
    }
    void OnTriggerStay2D(Collider2D collision)
    {        
        if(collision.name == "player"){
            Debug.Log($"Проверка стойки, требуемая стойка: {CurrentState}, стойка игрока: {playerController.State}");
            if(playerController.State == CurrentState){
                Collider.enabled = false;
            }
            else{
                Collider.enabled = true;
            }
        }
    }
}
