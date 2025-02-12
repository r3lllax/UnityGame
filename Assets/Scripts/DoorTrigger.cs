using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject text;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "player"){
            Debug.Log("label for player active!");
            text.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.name == "player"){
            Debug.Log("label for player disabled!");
            text.SetActive(false);
        }
    }
}
