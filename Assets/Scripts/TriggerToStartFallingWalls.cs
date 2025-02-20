using UnityEngine;

public class TriggerToStartFallingWalls : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player"){
            FallingWalls.toMove = true;
        }
    }
}
