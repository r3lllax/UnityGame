using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private float slowedPlayerSpeed = 1.5f;
    public static string CurrentPlatform;
    [SerializeField] private GameObject TriggerParentPlatform;
    private double currentPos;
    private static float DefaultPlayerSpeed;

    void Awake()
    {
        currentPos = TriggerParentPlatform.GetComponent<Transform>().position.x;
        currentPos = Math.Round(currentPos,5);
        CurrentPlatform = "SecondCell";
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player"){
            DefaultPlayerSpeed = collision.GetComponent<playerController>().speed;
            collision.GetComponent<playerController>().speed = slowedPlayerSpeed;
        }
        foreach(var Notation in FallingWalls.intervals){
            if(Notation.Value == currentPos){
                CurrentPlatform = Notation.Key;
                break;
            }

        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "player"){
            collision.GetComponent<playerController>().speed = 3;
        }
    }
}
